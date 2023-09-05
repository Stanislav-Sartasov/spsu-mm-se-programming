using System;
using System.Windows;
using System.Windows.Controls;
using WeatherGetter;

namespace WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		OpenWeather openWeather = new OpenWeather(new Request());
		TomorrowIo tomorrowIo = new TomorrowIo(new Request());

		public MainWindow()
		{
			InitializeComponent();
			UpdateData(tomorrowIoGrid, tomorrowIo);
			UpdateData(openWeatherGrid, openWeather);
		}

		private void UpdateButtonClick(object sender, RoutedEventArgs e)
		{
			UpdateData(tomorrowIoGrid, tomorrowIo);
			UpdateData(openWeatherGrid, openWeather);
		}

		private void ExitButtonClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void UpdateData(Grid grid, Site site)
		{
			string[] dataFromSite = new string[5];

			for (int i = 0; i < 5; i++)
			{
				((TextBlock)grid.Children[i]).Text = "";
			}

			try
			{
				dataFromSite = GetData(site.GetData());
				for (int i = 0; i < 5; i++)
					((TextBlock)grid.Children[i]).Text = dataFromSite[i];
			}
			catch (Exception ex)
			{
				((TextBlock)grid.Children[0]).Text = ex.Message;
			}
		}

		private string[] GetData(Weather weather)
		{
			return new string[]
			{
				"Temperature: " + CheckNull(weather.TemperatureCelsius) + "°C (" + CheckNull(weather.TemperatureFahrenheit) + "°F)",
				"Wind: " + CheckNull(weather.WindSpeed) + " m/s (" + CheckNull(weather.WindDirection) + ")",
				"Cloud coverage: " + CheckNull(weather.CloudCover) + "%",
				"Precipitation: " + CheckNull(weather.Precipitation),
				"Humidity: " + CheckNull(weather.Humidity) + "%"
			};
		}

		private static object CheckNull(object? data)
		{
			if (data != null)
				return data;
			return "No Data";
		}
	}
}
