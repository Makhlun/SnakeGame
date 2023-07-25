using Snake_v._0._0;
using System;
using static Snake_v._0._0.GameState;

namespace Snake_v._0._0
{
    [Serializable]
    public abstract class Level
    {
        protected Level level;

        public void SetNextLevel(Level level)
        {
            this.level = level;
        }

        public abstract void HandleLevel(PlayWindow playWin);
    }
    [Serializable]
    public class Level1 : Level
    {
        private bool isSpeedChanged = false;

        public override void HandleLevel(PlayWindow playWin)
        {
            if (playWin.gameState.Score < 4 && !playWin.settings.IsStaticSpeed)
            {
                if (!isSpeedChanged)
                {
                    playWin.gameState.Speed = 700;
                    isSpeedChanged = true;
                }

                playWin.Lvl.Text = "Level  :  1";
            }
            else if (level != null)
            {
                level.HandleLevel(playWin);
            }
        }
    }
    [Serializable]
    public class Level2 : Level
    {
        private bool isSpeedChanged = false;

        public override void HandleLevel(PlayWindow playWin)
        {
            if (playWin.gameState.Score >= 4 && playWin.gameState.Score < 9)
            {
                if (!isSpeedChanged)
                {
                    playWin.gameState.Speed -= 100;
                    isSpeedChanged = true;
                }
                playWin.Lvl.Text = "Level  :  2";
            }
            else if (level != null)
            {
                level.HandleLevel(playWin);
            }
        }
    }
    [Serializable]
    public class Level3 : Level
    {
        public override void HandleLevel(PlayWindow playWin)
        {
            if (playWin.gameState.Score >= 9 && playWin.gameState.Score < 12)
            {
                if (!playWin.settings.IsToxicFood) playWin.gameState.foodStrategy = new ToxicFoodStrategy();
                playWin.Lvl.Text = "Level  :  3";
            }
            else if (level != null)
            {
                level.HandleLevel(playWin);
            }
        }
    }
    [Serializable]
    public class Level4 : Level
    {
        private bool isSpeedChanged = false;
        public override void HandleLevel(PlayWindow playWin)
        {
            if (playWin.gameState.Score >= 12 && playWin.gameState.Score < 15)
            {
                if (!isSpeedChanged)
                {
                    playWin.gameState.Speed -= 100;
                    isSpeedChanged = true;
                }

                playWin.Lvl.Text = "Level  :  4";
            }
            else if (level != null)
            {
                level.HandleLevel(playWin);
            }
        }

    }
    [Serializable]
    public class Level5 : Level
    {
        private bool isSpeedChanged = false;

        public override void HandleLevel(PlayWindow playWin)
        {
            if (playWin.gameState.Score >= 15)
            {
                if (!isSpeedChanged)
                {
                    playWin.gameState.Speed -= 100;
                    isSpeedChanged = true;
                }
                playWin.Lvl.Text = "Level  :  5";
            }
            else if (level != null)
            {
                level.HandleLevel(playWin);
            }
        }
    }
}


