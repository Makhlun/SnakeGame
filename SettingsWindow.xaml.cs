using System.Windows;

namespace Snake_v._0._0
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
     
    public partial class SettingsWindow : Window
    {
        private MainWindow mainWindow;

        public SettingsWindow(Settings settings)
        {
            InitializeComponent();
            ApplySettings(settings);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new SettingsPrototype().Clone();

            if (small.IsEnabled == false) settings.Size=GridSize.Small;
            else if(middle.IsEnabled == false) settings.Size=GridSize.Middle;
            else if(big.IsEnabled==false) settings.Size=GridSize.Big;

            if (standart.IsEnabled == false) settings.IsToxicFood = false;
            else if (obstacle.IsEnabled == false) settings.IsToxicFood = true;

            if (staticSpeed.IsEnabled == false) settings.IsStaticSpeed = true;
            else if (dinamicSpeed.IsEnabled == false) settings.IsStaticSpeed = false;

            if (yellow.IsEnabled == false) settings.SnakeColor = new YellowSnakeFactory();
            else if (green.IsEnabled == false) settings.SnakeColor = new GreenSnakeFactory();
            else if (purple.IsEnabled == false) settings.SnakeColor = new PurpleSnakeFactory();
            else if (cyan.IsEnabled == false) settings.SnakeColor = new CyanSnakeFactory();

            mainWindow = new MainWindow();
            mainWindow.SetSettings(settings);
            mainWindow.Show();
            this.Close();
        }

        private void small_Click(object sender, RoutedEventArgs e)
        {
            small.IsEnabled = false;
            middle.IsEnabled = true;
            big.IsEnabled = true;
        }

        private void middle_Click(object sender, RoutedEventArgs e)
        {
            small.IsEnabled = true;
            middle.IsEnabled = false;
            big.IsEnabled = true;
        }

        private void big_Click(object sender, RoutedEventArgs e)
        {
            small.IsEnabled = true;
            middle.IsEnabled = true;
            big.IsEnabled = false;
        }

        private void standart_Click(object sender, RoutedEventArgs e)
        {
            standart.IsEnabled = false;
            obstacle.IsEnabled = true;
        }

        private void obstacle_Click(object sender, RoutedEventArgs e)
        {
            standart.IsEnabled = true;
            obstacle.IsEnabled = false;
        }

        private void staticSpeed_Click(object sender, RoutedEventArgs e)
        {
            staticSpeed.IsEnabled = false;
            dinamicSpeed.IsEnabled = true;
        }

        private void dinamicSpeed_Click(object sender, RoutedEventArgs e)
        {
            staticSpeed.IsEnabled = true;
            dinamicSpeed.IsEnabled = false;
        }


        private void yellow_Click(object sender, RoutedEventArgs e)
        {
            yellow.IsEnabled = false;
            green.IsEnabled = true;
            purple.IsEnabled = true;
            cyan.IsEnabled = true;
        }

        private void green_Click(object sender, RoutedEventArgs e)
        {
            yellow.IsEnabled = true;
            green.IsEnabled = false;
            purple.IsEnabled = true;
            cyan.IsEnabled = true;
        }
        private void purple_Click(object sender, RoutedEventArgs e)
        {
            yellow.IsEnabled = true;
            green.IsEnabled = true;
            purple.IsEnabled = false;
            cyan.IsEnabled = true;
        }

        private void cyan_Click(object sender, RoutedEventArgs e)
        {
            yellow.IsEnabled = true;
            green.IsEnabled = true;
            purple.IsEnabled = true;
            cyan.IsEnabled = false;
        }

        private void ApplySettings(Settings settings)
        {
            // Встановлюємо значення кнопок відповідно до переданих налаштувань
            small.IsEnabled = settings.Size != GridSize.Small;
            middle.IsEnabled = settings.Size != GridSize.Middle;
            big.IsEnabled = settings.Size != GridSize.Big;

            standart.IsEnabled = settings.IsToxicFood;
            obstacle.IsEnabled = !settings.IsToxicFood;

            staticSpeed.IsEnabled = !settings.IsStaticSpeed;
            dinamicSpeed.IsEnabled = settings.IsStaticSpeed;

            yellow.IsEnabled = settings.SnakeColor.GetColor() != SnakeColor.Yellow;
            green.IsEnabled = settings.SnakeColor.GetColor() != SnakeColor.Green;
            purple.IsEnabled = settings.SnakeColor.GetColor() != SnakeColor.Purple;
            cyan.IsEnabled = settings.SnakeColor.GetColor() != SnakeColor.Cyan;
        }
    }
}
