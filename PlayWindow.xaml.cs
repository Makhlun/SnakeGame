using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Snake_v._0._0
{
    /// <summary>
    /// Interaction logic for PlayWindow.xaml
    /// </summary>
    [Serializable]
    public partial class PlayWindow : Window
    {
        private MainWindow mainWindow;
        public Settings settings;

        private Dictionary<GridValue, ImageSource> gridValToImage;

        private readonly Dictionary<Direction, int> directToRotation = new Dictionary<Direction, int>
        {
            {Direction.Up, 0},
            {Direction.Right, 90},
            {Direction.Down, 180},
            {Direction.Left, -90}
        };

        public Level level1 = new Level1();
        private Level level2 = new Level2();
        private Level level3 = new Level3();
        private Level level4 = new Level4();
        private Level level5 = new Level5();

        public int rows, cols;
        private Image[,] gridImages;
        public GameState gameState;
        private IPlayingState state = new GameStart();
        private Invoker _invoker;

        public PlayWindow(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
            rows = (int)settings.Size;
            cols = (int)settings.Size;

            if (!settings.IsStaticSpeed)
            {
                SetLevels();
            }

            _invoker = new Invoker();
            gridImages = SetupGrid();
            SetColorOfSnake();
            gameState = new GameState(rows, cols, GetMode());
        }

        private bool GetMode() => settings.IsToxicFood;

        private void SetLevels()
        {
            level1.SetNextLevel(level2);
            level2.SetNextLevel(level3);
            level3.SetNextLevel(level4);
            level4.SetNextLevel(level5);
        }

        private void SetColorOfSnake()
        {
            gridValToImage = new Dictionary<GridValue, ImageSource>(){
            {GridValue.Empty, Images.Empty },
            {GridValue.Food, Images.Food},
            {GridValue.ToxicFood, Images.ToxicFood },
            { GridValue.Snake, settings.SnakeColor.CreateSnakeColorFactory().Body()} };
        }

        private Image[,] SetupGrid()
        {
            GameGrid.Children.Clear();

            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;
            GameGrid.Width = GameGrid.Height * (cols / (double)rows);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image image = new Image
                    {
                        Source = Images.Empty,
                        RenderTransformOrigin = new Point(0.5, 0.5)
                    };

                    images[r, c] = image;
                    GameGrid.Children.Add(image);
                }
            }

            return images;
        }

        private void Draw()
        {
            DrawGrid();
            DrawSnakeHead();
            ScoreText.Text = $"SCORE :  {gameState.Score}";
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    GridValue gridValue = gameState.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridValue];
                    gridImages[r, c].RenderTransform = Transform.Identity;
                }
            }
        }

        private void DrawSnakeHead()
        {
            Position headPos = gameState.HeadPosition();
            Image image = gridImages[headPos.Row, headPos.Column];
            image.Source = settings.SnakeColor.CreateSnakeColorFactory().Head();

            int rotation = directToRotation[gameState.Direction];
            image.RenderTransform = new RotateTransform(rotation);
        }

        private async Task DrawDeadSnake()
        {
            List<Position> positions = new List<Position>(gameState.SnakePosition());

            for (int i = 0; i < positions.Count; i++)
            {
                Position pos = positions[i];
                ImageSource sourse = (i == 0) ? settings.SnakeColor.CreateSnakeColorFactory().DeadHead() : settings.SnakeColor.CreateSnakeColorFactory().DeadBody();
                gridImages[pos.Row, pos.Column].Source = sourse;
                await Task.Delay(25);
            }
        }

        private async Task RunGame()
        {
            Draw();
            await ShowCountDown();
            state.ChangeOverlay(this);
            await GameLoop();
            await ShowGameOver();
            gameState = new GameState(rows, cols, GetMode());
        }

        private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Overlay.Visibility == Visibility.Visible)
            {
                e.Handled = true;
            }

            if (!state.IsActive() && state.State() == PlayState.GamePause)
            {
                await ShowCountDown();
                state = new GameActive();
                state.ChangeOverlay(this);
            }

            else if (!state.IsActive())
            {
                state = new GameActive();
                await RunGame();
                state = new GameOverState();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (state.State() == PlayState.GameOver || state.State() == PlayState.GamePause) return;
            switch (e.Key)
            {
                case Key.Left:
                    gameState.ChangeDirection(Direction.Left);
                    break;
                case Key.Up:
                    gameState.ChangeDirection(Direction.Up);
                    break;
                case Key.Right:
                    gameState.ChangeDirection(Direction.Right);
                    break;
                case Key.Down:
                    gameState.ChangeDirection(Direction.Down);
                    break;
                case Key.R:
                    RestartGame();
                    break;
                case Key.P:
                    PauseGame();
                    break;
            }
        }

        private async Task GameLoop()
        {
            while (!gameState.GameOver)
            {
                level1.HandleLevel(this);
                if (!state.IsActive())
                {
                    while (state.State() == PlayState.GamePause)
                    {
                        await Task.Delay(200);
                    }
                }
                else
                {
                    await Task.Delay(gameState.Speed);
                    gameState.Move();
                    Draw();
                }
            }
        }

        private async Task ShowCountDown()
        {
            for (int i = 3; i >= 1; i--)
            {
                OverlayText.Text = i.ToString();
                await Task.Delay(500);
            }
            await Task.Delay(100);
        }

        private async Task ShowGameOver()
        {
            await DrawDeadSnake();
            await Task.Delay(1000);
            state = new GameOverState();
            state.ChangeOverlay(this);
        }

        private void Restart_Click(object sender, RoutedEventArgs e) => RestartGame();

        private void RestartGame()
        {
            gameState.GameOver = true;
            state = new GameOverState();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            PauseGame();
        }

        private void PauseGame()
        {
            if (state.State() != PlayState.GameActive) return;
            
            state = new GamePause();
            state.ChangeOverlay(this);
        }

        public void Restore(GameState gameState, int rows, int cols, Level level, Settings settings)
        {
            PauseGame();
            this.rows = rows;
            this.cols = cols;
            this.level1 = level;
            state.ChangeOverlay(this);
            this.settings = settings;
            this.gameState = new GameState(rows, cols, GetMode(), gameState);
            gridImages = SetupGrid();
            SetColorOfSnake();
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            ICommand restore = new RestoreCommand(this);
            _invoker.SetCommand(restore);
            _invoker.ExecuteCommand();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ICommand save = new SaveCommand(this);
            _invoker.SetCommand(save);
            _invoker.ExecuteCommand();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.SetSettings(settings);
            mainWindow.Show();
            this.Close();
        }
    }
}
