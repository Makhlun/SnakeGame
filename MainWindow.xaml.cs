using System.Windows;

namespace Snake_v._0._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
     
    public partial class MainWindow : Window
    {
        private PlayWindow playWindow;
        private SettingsWindow settingsWindow;
        private Settings settings = new SettingsPrototype().SettingsProt;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetSettings(Settings settings)
        {
            this.settings = settings;
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            playWindow = new PlayWindow(settings);
            playWindow.Show();
            this.Close();
        }
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            settingsWindow = new SettingsWindow(settings);
            settingsWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
