using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using WeatherLib.Container;
using WeatherLib.ISite;
using WeatherLib.Sites.OpenWeatherMap;
using WeatherLib.Sites.TomorrowIo;
using WeatherLib.WeatherInfo;

namespace WPF
{
    public partial class MainWindow : Window
    {
        private List<IWeatherService> services = IoCContainer.GetServices();

        public MainWindow()
        {
            InitializeComponent();
            services.Find(x => x is OpenWeatherMapWeatherService).CurrentWeather = (Weather)Resources["OpenWeatherMap"];
            services.Find(x => x is TomorrowIoWeatherService).CurrentWeather = (Weather)Resources["TomorrowIo"];
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            UpdateButton.IsEnabled = false;
            UpdateButton.Content = "Loading...";
            Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)delegate
            {
                foreach (var service in services)
                {
                    service.UpdateWeather();
                }
                Time.Text = "Weather current at " + DateTime.Now.ToString("HH:mm");
                UpdateButton.Content = "Update info";
                UpdateButton.IsEnabled = true;
            });
        }
    }
}