using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Snake_v._0._0
{
    public interface ICommand
    {
        void Execute(string path);
    }

    [Serializable]
    public class Invoker
    {
        private static string path = "D:\\SavedData\\SavedData.bin";

        private ICommand _command;

        public void SetCommand(ICommand command) => _command = command;

        public void ExecuteCommand() => _command.Execute(path);
    }
    [Serializable]
    public class SaveCommand : ICommand
    {
        private PlayWindow _playWindow;
        private CreateMemento _memento = new CreateMemento();

        public SaveCommand(PlayWindow playWindow)
        {
            _playWindow = playWindow;
        }

        public void Execute(string path)
        {
            _memento.SaveMemento( new SaveMemento(
                _playWindow.gameState,
                _playWindow.rows,
                _playWindow.cols,
                _playWindow.level1,
                _playWindow.settings
                ));

            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                binaryFormatter.Serialize(fileStream, _memento);
            }
        }
    }
    [Serializable]
    public class RestoreCommand : ICommand
    {
        private PlayWindow _playWindow;

        public RestoreCommand(PlayWindow playWindow)
        {
            _playWindow = playWindow;
        }

        public void Execute(string path)
        {
            SaveMemento restoreMemento;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                restoreMemento = ((CreateMemento)binaryFormatter.Deserialize(fileStream)).GetMemento();
            }

            _playWindow.Restore(
                restoreMemento.GameState,
                restoreMemento.Rows,
                restoreMemento.Cols,
                restoreMemento.Level,
                restoreMemento.Settings
                );
        }
    }
}
