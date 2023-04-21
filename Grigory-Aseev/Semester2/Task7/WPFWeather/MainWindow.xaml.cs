using System;
using System.Windows;
using System.Windows.Input;
using UITools;
using WPFWeather.Properties;
using Sites;

namespace WPFWeather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SiteData Data { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            Data = Settings.Default["Site"].ToString() switch
            {
                "openweathermap" => new SiteData(typeof(OpenWeatherMap)),
                "stormglassio" => new SiteData(typeof(StormGlassIO)),
                _ => new SiteData(typeof(TomorrowIO))
            };

            DrawTemperature();
            DataContext = Data;
        }

        private void ClickToRightSide(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ClickTempC(object sender, RoutedEventArgs e)
        {
            Settings.Default["Temperature"] = "C";
            Settings.Default.Save();
            DrawTemperature();
        }

        private void ClickTempF(object sender, RoutedEventArgs e)
        {
            Settings.Default["Temperature"] = "F";
            Settings.Default.Save();
            DrawTemperature();
        }

        private void ClickExit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void UpdateOpenWeather(object sender, RoutedEventArgs e)
        {
            UpdateSite(typeof(OpenWeatherMap));
        }

        private void UpdateTomorrow(object sender, RoutedEventArgs e)
        {
            UpdateSite(typeof(TomorrowIO));
        }

        private void UpdateStormGlass(object sender, RoutedEventArgs e)
        {
            UpdateSite(typeof(StormGlassIO));
        }

        private void UpdateSite(Type type)
        {
            Data.UpdateData(type);
            DrawTemperature();
            Settings.Default["Site"] = type.Name.ToString().ToLower();
            Settings.Default.Save();
        }

        private void DrawTemperature()
        {
            if (Data.Exception is not null)
            {
                temperature.Text = null;
                return;
            }

            if (Settings.Default["Temperature"].ToString().Equals("C"))
            {
                temperature.Text = $"{Data.Forecast?.TemperatureInCelsius} °C";
            }
            else
            {
                temperature.Text = $"{Data.Forecast?.TemperatureInFahrenheit} °F";
            }
        }
    }
}
