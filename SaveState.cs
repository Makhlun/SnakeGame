using System;

namespace Snake_v._0._0
{
    // Знімок (Memento)
    [Serializable]
    public class SaveMemento
    {
        public GameState GameState { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public Level Level { get; private set; }
        public Settings Settings { get; private set; }

        public SaveMemento(GameState gameState, int rows, int cols,
            Level level, Settings settings)
        {
            GameState = gameState;
            Rows = rows;
            Cols = cols;
            Level = level;
            Settings = settings;
        }
    }

    // Творець (Caretaker)
    [Serializable]
    public class CreateMemento
    {
        private SaveMemento _memento;

        public void SaveMemento(SaveMemento memento) => _memento = memento;

        public SaveMemento GetMemento() => _memento;
    }
}
