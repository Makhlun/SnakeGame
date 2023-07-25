using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Windows.Media;
using System.Reflection;

namespace Snake_v._0._0
{
    [Serializable]
    public class GameState
    {
        public int Rows { get; }
        public int Columns { get; }
        public GridValue[,] Grid { get; }
        public Direction Direction { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; set; }
        public int Speed { get; set; }
        public IFoodStrategy foodStrategy;

        private readonly LinkedList<Direction> directionChanges = new LinkedList<Direction>();
        private readonly LinkedList<Position> snakePosition = new LinkedList<Position>();

        private int countOfToxicFood = 0;
        private readonly Random random = new Random();

        public GameState(int rows, int columns, bool isDangerMode)
        { 
            Rows = rows;
            Columns = columns;
            Grid = new GridValue[rows, columns];
            Direction = Direction.Right;
            Speed = 500;
            AddSnake();
            SetFoodStrategy(isDangerMode);
            AddFood();
        }

        public GameState(int rows, int columns, bool isDangerMode, GameState gameState)
        {
            Rows = rows;
            Columns = columns;
            Grid = gameState.Grid;
            Direction = gameState.Direction;
            Speed = gameState.Speed;
            Score = gameState.Score;
            SetFoodStrategy(isDangerMode);
            snakePosition = gameState.snakePosition;
        }

        private void SetFoodStrategy(bool dangerMode)
        {
            if (dangerMode)
            {
                foodStrategy = new ToxicFoodStrategy();
            }
            else foodStrategy = new FoodStrategy();
        }

        private void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePosition.AddFirst(new Position(r, c));
            }
        }

        private IEnumerable<Position> EmptyPosition()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPosition());
            if (empty.Count == 0) return;

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.Food;
        }

        private async Task AddToxicFood()
        {
            List<Position> empty = new List<Position>(EmptyPosition());

            if (empty.Count == 0) return;

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Column] = GridValue.ToxicFood;
            countOfToxicFood++;
            await Task.Delay(7000);
            Grid[pos.Row, pos.Column] = GridValue.Empty;
            countOfToxicFood--;
        }

        public Position HeadPosition() => snakePosition.First.Value;
        
        public Position TailPosition()=> snakePosition.Last.Value;

        public IEnumerable<Position> SnakePosition()=> snakePosition;

        private void AddHead(Position position)
        {
            snakePosition.AddFirst(position);
            Grid[position.Row, position.Column] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePosition.Last.Value;
            Grid[tail.Row, tail.Column] = GridValue.Empty;
            snakePosition.RemoveLast();
        }

        private Direction GetLastDirection()
        {
            if (directionChanges.Count == 0)
            {
                return Direction;
            }
            return directionChanges.Last.Value;
        }

        private bool CanChangeDirection(Direction newDirection)
        {
            if (directionChanges.Count == 2)
            {
                return false;
            }

            Direction lastDirection = GetLastDirection();
            return newDirection != lastDirection && newDirection != lastDirection.Opposite();
        }

        public void ChangeDirection(Direction direction)
        {
            if (CanChangeDirection(direction))
            {
                directionChanges.AddLast(direction);
            }
        }

        private bool OutsideGrid(Position position)
        {
            return position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns;
        }

        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            if (newHeadPos == TailPosition())
            {
                return GridValue.Empty;
            }

            return Grid[newHeadPos.Row, newHeadPos.Column];
        }

        public async void Move()
        {
            if (directionChanges.Count > 0)
            {
                Direction = directionChanges.First.Value;
                directionChanges.RemoveFirst();
            }

            Position newHeadPos = HeadPosition().Translate(Direction);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
            }

            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }

            else if (hit == GridValue.ToxicFood)
            {
                RemoveTail();
                AddHead(newHeadPos);
                foodStrategy.ProcessToxicFood(this);
            }

            else if (hit == GridValue.Food)
            {
                AddHead(newHeadPos);
                foodStrategy.ProcessGoodFood(this);
                foodStrategy.AddFood(this);
            }

            if (countOfToxicFood == 0)
                await foodStrategy.AddToxicFood(this);
        }

        // Strategy
        public interface IFoodStrategy
        {
            void AddFood(GameState gameState);
            Task AddToxicFood(GameState gameState);
            void ProcessGoodFood(GameState gameState);
            void ProcessToxicFood(GameState gameState);
        }
        [Serializable]
        public class FoodStrategy : IFoodStrategy
        {
            private SoundPlayer soundPlayer = new SoundPlayer(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds/EatFood.wav"));  
            public void AddFood(GameState gameState)
            {
                gameState.AddFood();
            }

            public async Task AddToxicFood(GameState gameState) {/*Nothing to do*/ }

            public void ProcessGoodFood(GameState gameState)
            {
                soundPlayer.Play();
                gameState.Score++;
            }

            public void ProcessToxicFood(GameState gameState) {/*Nothing to do*/ }

        }
        [Serializable]
        public class ToxicFoodStrategy : IFoodStrategy
        {
            private SoundPlayer soundPlayerFood = new SoundPlayer(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds/EatFood.wav"));
            private SoundPlayer soundPlayerToxicFood = new SoundPlayer(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Sounds/EatToxicFood.wav"));

            public void AddFood(GameState gameState)
            {
                gameState.AddFood();
            }

            public async Task AddToxicFood(GameState gameState)
            {
                await gameState.AddToxicFood();
            }

            public void ProcessGoodFood(GameState gameState)
            {
                soundPlayerFood.Play();
                gameState.Score++;
            }

            public void ProcessToxicFood(GameState gameState)
            {
                soundPlayerToxicFood.Play();
                if (gameState.Speed >= 150)
                {
                    gameState.Speed -= 100;
                }
            }
        }
    }
}
