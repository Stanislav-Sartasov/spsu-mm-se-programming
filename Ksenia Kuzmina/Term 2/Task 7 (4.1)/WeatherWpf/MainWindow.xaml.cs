using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Weather;
using Weather.Parsers;

namespace WeatherWpf
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadWeather();
		}

		private async void UpdateButtonClick(object sender, RoutedEventArgs e)
		{
			LoadWeather();
		}

		private void ExitButtonClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private async void LoadWeather()
		{
			MyHttpClient client = new MyHttpClient();

			IParser parserTomorrowIo = new ParserTomorrowIo(client);
			IParser parserOpenWeather = new ParserOpenWeather(client);

			Weather.Weather weatherTomorrowIo;
			Weather.Weather weatherOpenWeather;

			try
			{
				weatherTomorrowIo = await GetWeather(parserTomorrowIo);
			}
			catch
			{
				weatherTomorrowIo = null;
			}

			try
			{
				weatherOpenWeather = await GetWeather(parserOpenWeather);
			}
			catch
			{
				weatherOpenWeather = null;
			}

			UpdateGrid(tomorrowIoGrid, weatherTomorrowIo);

			UpdateGrid(openWeatherGrid, weatherOpenWeather);
		}

		private void UpdateGrid(Grid grid, Weather.Weather? weather)
		{
			if (weather == null)
			{
				foreach (var child in grid.Children)
					if(child is TextBlock)
						((TextBlock)child).Text = "No Data";

				return;
			}

			((TextBlock)grid.Children[0]).Text = weather.CelsiusTemperature.ToString() + "°";
			((TextBlock)grid.Children[1]).Text = weather.FahrenheitTemperature.ToString() + "°";
			((TextBlock)grid.Children[2]).Text = weather.CloudCover.ToString() + "%";
			((TextBlock)grid.Children[3]).Text = weather.Precipitation.ToString();
			((TextBlock)grid.Children[4]).Text = weather.Humidity.ToString() + "%";
			((TextBlock)grid.Children[5]).Text = weather.WindDirection.ToString();
			((TextBlock)grid.Children[6]).Text = weather.WindSpeed.ToString() + "m/s";
		}

		private async Task<Weather.Weather> GetWeather(IParser parser)
		{
			Weather.Weather weather;

			weather = await parser.GetWeatherInfoAsync();

			return weather;
		}
	}
}
