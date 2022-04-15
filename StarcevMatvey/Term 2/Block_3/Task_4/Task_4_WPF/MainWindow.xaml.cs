using System.Collections.Generic;
using System.Windows;
using ISites;
using Sites;

namespace Task_4
{
    public partial class MainWindow : Window
    {
        readonly List<ISite> sites = new List<ISite> { new OpenWeatherMap(), new TomorrowIo() };
        readonly int countOfSites = 2;
        private int index = 0;

        public MainWindow()
        {
            InitializeComponent();

            ISite site = sites[index % countOfSites];
            siteName.Text = site.Name;
            SetWeather(site.GetWeather());
        }

        private void SwitchClick(object sender, RoutedEventArgs e)
        {
            ISite site = sites[++index % countOfSites];
            Weather.Weather weather = site.GetWeather();
            SetWeather(weather);
            siteName.Text = site.Name;
            MessageBox.Show("Swithed");
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            ISite site = sites[index % countOfSites];
            Weather.Weather weather = site.GetWeather();
            SetWeather(weather);
            MessageBox.Show("Refreshed");
        }

        private void SetWeather(Weather.Weather weather)
        {
            tempValue.Text = weather.TempC + " " + weather.TempF;
            cloudsValue.Text = weather.Clouds;
            humidityValue.Text = weather.Humidity;
            windSpdValue.Text = weather.WindSpeed;
            windDegValue.Text = weather.WindDegree;
            falloutValue.Text = weather.FallOut;
        }
    }
}
