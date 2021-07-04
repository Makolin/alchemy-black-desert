using System.Windows;

namespace Alchemy_Black_Desert.Pages
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Content = new MainPage();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CraftButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new CraftPage();
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new OptionsPage();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new InfoPage();
        }

        private void MainButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new MainPage();
        }
    }
}
