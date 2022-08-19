using System;
using System.Windows;
using System.Windows.Input;
using UIWeatherForecast;
using WeatherManagerAPI;

namespace WpfWeatherForecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeatherForecast WeatherData { get; private set; }
        public static MainWindow Window;     
        public MainWindow()
        {
            InitializeComponent();
            Window = this;
            WeatherData = new WeatherForecast();
            DataContext = WeatherData.ForecastData;
        }

        private void Drag(object sender, RoutedEventArgs e)
        {
            if(Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Window.DragMove();
            }    
        }

        private void NavPanelLocation(int top)
        {
            NavPanel.Height = 53;
            NavPanel.Margin = new Thickness(0, top, 0, 0);

        }

        private void BtnMainInfo_Click(object sender, RoutedEventArgs e)
        {
            NavPanelLocation(215);
            WeatherData.ForecastData = null;
            DataContext = WeatherData.ForecastData;
            GetMainMenuInfo();
        }

        private void BtnTomorrowio_Click(object sender, RoutedEventArgs e)
        {
            NavPanelLocation(268);
            GetWeatherData(typeof(TomorrowIO));
        }

        private void BtnStormGlassio_Click(object sender, RoutedEventArgs e)
        {
            NavPanelLocation(321);
            GetWeatherData(typeof(StormGlassIO));
        }

        private void GetWeatherData(Type type)
        {
            WeatherData.UpdateWeather(type);
            if (WeatherData.ExceptionMessage != null)
            {
                WeatherData.ForecastData = null;
                ExceptionBorder.DataContext = WeatherData;
            }
            DataContext = WeatherData.ForecastData;
        }

        private void GetMainMenuInfo()
        {
            if(WeatherData.ExceptionMessage == null)
            {
                WeatherData.ExceptionMessage = "This program is designed to obtain weather data in the city of St. Petersburg from sites such as:\t " +
                    "Tomorrow.Io, \tStormGlass.io\t\t\t\t\t\t    Hover over a widget to get information about it";
                ExceptionBorder.DataContext = WeatherData;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
