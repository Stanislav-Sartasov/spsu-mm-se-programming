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
using Task_5;

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
			UpdateData();
		}

		private void UpdateButtonClick(object sender, RoutedEventArgs e)
		{
			UpdateData();
		}

		private void ExitButtonClick(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void UpdateData()
		{
			string[] dataFromTomorrowIo = new string[5];
			string[] dataFromOpenWeather = new string[5];

			for (int i = 0; i < 5; i++)
			{
				((TextBlock)tomorrowIoGrid.Children[i]).Text = "";
				((TextBlock)openWeatherGrid.Children[i]).Text = "";
			}

			try
			{
				dataFromTomorrowIo = GetData(tomorrowIo.GetData());
				for (int i = 0; i < 5; i++)
					((TextBlock)tomorrowIoGrid.Children[i]).Text = dataFromTomorrowIo[i];
			}
			catch (Exception ex)
			{
				((TextBlock)tomorrowIoGrid.Children[0]).Text = ex.Message;
			}

			try
			{
				dataFromOpenWeather = GetData(openWeather.GetData());
				for (int i = 0; i < 5; i++)
					((TextBlock)openWeatherGrid.Children[i]).Text = dataFromOpenWeather[i];
			}
			catch (Exception ex)
			{
				((TextBlock)openWeatherGrid.Children[0]).Text = ex.Message;
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
