using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherLib.Parsers;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;

namespace WeatherWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private async void RefreshDataAfterPictureClick(object sender, MouseButtonEventArgs e)
		{
			IWeatherService openweather = new Openweather();
			IWeatherService stormglass = new Stormglass();

			ResponceReceiver receiver = new ResponceReceiver();

			await receiver.GetResponce(openweather.URL);
			WeatherForecast oForecast = openweather.GetWeatherForecast(receiver);

			await receiver.GetResponce(stormglass.URL);
			WeatherForecast sForecast = stormglass.GetWeatherForecast(receiver);

			RefreshLabelsText(leftStackPanel, oForecast);
			RefreshLabelsText(rightStackPanel, sForecast);
		}

		private void RefreshLabelsText(Panel weatherStackPanel, WeatherForecast forecast)
		{
			if (forecast == null)
			{
				foreach (Label label in weatherStackPanel.Children)
					label.Content = "...";

				((Label)weatherStackPanel.Children[6]).Content = "service is not\navailable";

				return;
			}

			forecast.PrepareForUI();

			((Label)weatherStackPanel.Children[0]).Content = forecast.Temperature;
			((Label)weatherStackPanel.Children[1]).Content = forecast.CloudCover;
			((Label)weatherStackPanel.Children[2]).Content = forecast.Humidity;
			((Label)weatherStackPanel.Children[3]).Content = forecast.Precipitation;
			((Label)weatherStackPanel.Children[4]).Content = forecast.WindSpeed;
			((Label)weatherStackPanel.Children[5]).Content = forecast.WindDirection;
			((Label)weatherStackPanel.Children[6]).Content = forecast.SourceSercvice.ToString();
		}
	}
}
