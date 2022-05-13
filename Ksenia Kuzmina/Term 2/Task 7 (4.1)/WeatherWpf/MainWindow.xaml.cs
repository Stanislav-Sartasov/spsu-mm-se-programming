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

			Weather.Weather _weatherTomorrowIo;
			Weather.Weather _weatherOpenWeather;

			try
			{
				_weatherTomorrowIo = await GetWeather(parserTomorrowIo);
			}
			catch
			{
				_weatherTomorrowIo = null;
			}

			try
			{
				_weatherOpenWeather = await GetWeather(parserOpenWeather);
			}
			catch
			{
				_weatherOpenWeather = null;
			}


			if (_weatherTomorrowIo != null)
			{
				TomorrowIoCelsius.Text = _weatherTomorrowIo.CelsiusTemperature.ToString() + "°";
				TomorrowIoFahrenheit.Text = _weatherTomorrowIo.FahrenheitTemperature.ToString() + "°";
				TomorrowIoCloudCover.Text = _weatherTomorrowIo.CloudCover.ToString() + "%";
				TomorrowIoPrecipitation.Text = _weatherTomorrowIo.Precipitation.ToString();
				TomorrowIoHumidity.Text = _weatherTomorrowIo.Humidity.ToString() + "%";
				TomorrowIoWindDirection.Text = _weatherTomorrowIo.WindDirection.ToString();
				TomorrowIoWindSpeed.Text = _weatherTomorrowIo.WindSpeed.ToString() + "m/s";
			}
			else
			{
				TomorrowIoCelsius.Text = "No data";
				TomorrowIoFahrenheit.Text = "No data";
				TomorrowIoCloudCover.Text = "No data";
				TomorrowIoPrecipitation.Text = "No data";
				TomorrowIoHumidity.Text = "No data";
				TomorrowIoWindDirection.Text = "No data";
				TomorrowIoWindSpeed.Text = "No data";
			}

			if (_weatherOpenWeather != null)
			{
				OpenWeatherCelsius.Text = _weatherOpenWeather.CelsiusTemperature.ToString() + "°";
				OpenWeatherFahrenheit.Text = _weatherOpenWeather.FahrenheitTemperature.ToString() + "°";
				OpenWeatherCloudCover.Text = _weatherOpenWeather.CloudCover.ToString() + "%";
				OpenWeatherPrecipitation.Text = _weatherOpenWeather.Precipitation.ToString();
				OpenWeatherHumidity.Text = _weatherOpenWeather.Humidity.ToString() + "%";
				OpenWeatherWindDirection.Text = _weatherOpenWeather.WindDirection.ToString();
				OpenWeatherWindSpeed.Text = _weatherOpenWeather.WindSpeed.ToString() + "m/s";
			}
			else
			{
				OpenWeatherCelsius.Text = "No data";
				OpenWeatherFahrenheit.Text = "No data";
				OpenWeatherCloudCover.Text = "No data";
				OpenWeatherPrecipitation.Text = "No data";
				OpenWeatherHumidity.Text = "No data";
				OpenWeatherWindDirection.Text = "No data";
				OpenWeatherWindSpeed.Text = "No data";
			}

		}

		private async Task<Weather.Weather> GetWeather(IParser parser)
		{
			Weather.Weather weather;

			weather = await parser.GetWeatherInfoAsync();

			return weather;
		}
	}
}
