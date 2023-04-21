using System.Windows;
using TomorrowIOAPI;
using StormglassIOAPI;
using WebWeatherRequester;
using WeatherRequesterResourceLibrary;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;

namespace WeatherAppWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private IWeatherRequester[] weatherSources = new IWeatherRequester[2];
		private Grid[] weatherGrids = new Grid[2];
		private List<Label>[] weatherDataLabels = new List<Label>[2];

		public MainWindow()
		{
			InitializeComponent();

			weatherSources[0] = new WebWeather(new TomorrowIOHandler(TomorrowIOResources.APIKey));
			weatherSources[1] = new WebWeather(new StormglassIOHandler(StormglassIOResources.APIKey));

			weatherGrids[0] = tomorrowGrid;
			weatherGrids[1] = stormglassGrid;

			weatherDataLabels[0] = new List<Label>();
			weatherDataLabels[1] = new List<Label>();

			for (int i = 0; i < 2; i++)
			{
				foreach (Label label in weatherGrids[i].Children)
					weatherDataLabels[i].Add(label);
			}

			UpdateWeather();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			UpdateWeather();
		}

		private void UpdateWeather()
		{
			for (int i = 0; i < 2; i++)
			{
				var weatherData = weatherSources[i].FetchWeatherData();

				if (weatherData == null)
				{
					weatherDataLabels[i][7].Content = "Error. Showing last data";
					weatherDataLabels[i][7].Foreground = Brushes.Red;
					continue;
				}
				weatherDataLabels[i][7].Content = "Success";
				weatherDataLabels[i][7].Foreground = Brushes.Green;

				weatherDataLabels[i][0].Content = $"{weatherData.TempC:F0}°";
				weatherDataLabels[i][1].Content = $"{weatherData.TempF:F0}°";
				weatherDataLabels[i][2].Content = $"{weatherData.Humidity}%";
				weatherDataLabels[i][3].Content = $"{weatherData.CloudCover}%";
				weatherDataLabels[i][4].Content = $"{weatherData.WindSpeed:F2} m\\s";
				weatherDataLabels[i][5].Content = $"{weatherData.WindDirection}";
				weatherDataLabels[i][6].Content = $"{weatherData.Precipitation}";
			}
		}
	}
}
