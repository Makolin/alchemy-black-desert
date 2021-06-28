using System.Windows;
using Alchemy_Black_Desert.Pages;

namespace Alchemy_Black_Desert
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
