using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_v._0._0
{
    public interface IPlayingState
    {
        bool IsActive();
        PlayState State();
        void ChangeOverlay(PlayWindow playWindow);
    }

    public enum PlayState
    {
        GameOver,
        GamePause,
        GameActive,
        GameStart
    }
     
    class GameOverState : IPlayingState
    {
        public bool IsActive() => false;
        public PlayState State()=> PlayState.GameOver;

        public void ChangeOverlay(PlayWindow playWindow)
        {
            playWindow.Overlay.Visibility = System.Windows.Visibility.Visible;
            playWindow.OverlayText.Text = "Press any KEY to START";
        }
    }
     
    class GamePause : IPlayingState
    {
        public bool IsActive() => false;

        public PlayState State() => PlayState.GamePause;
        public void ChangeOverlay(PlayWindow playWindow)
        {
            playWindow.Overlay.Visibility = System.Windows.Visibility.Visible;
            playWindow.OverlayText.Text = "Press any KEY to CONTINUE";

        }
    }
     
    class GameActive : IPlayingState
    {
        public bool IsActive() => true;

        public PlayState State() => PlayState.GameActive;
        public void ChangeOverlay(PlayWindow playWindow)
        {
            playWindow.Overlay.Visibility = System.Windows.Visibility.Hidden;
        }
    }
     
    class GameStart : IPlayingState
    {
        public bool IsActive() => false;
        public PlayState State() => PlayState.GameStart;

        public void ChangeOverlay(PlayWindow playWindow)
        {
            playWindow.Overlay.Visibility = System.Windows.Visibility.Visible;
            playWindow.OverlayText.Text = "Press any KEY to START";
        }
    }
}
