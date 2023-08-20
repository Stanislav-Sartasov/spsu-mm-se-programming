using System;
using System.Windows;

namespace Task_1.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            MainWindow firstAdditionalWindow = new MainWindow();
            MainWindow secondAdditionalWindow = new MainWindow();

            firstAdditionalWindow.Show();
            secondAdditionalWindow.Show();
        }
    }
}
