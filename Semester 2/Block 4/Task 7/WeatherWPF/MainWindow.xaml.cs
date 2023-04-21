using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeatherLib.Parsers;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;
using System.Collections.Generic;
using System.Linq;

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

		private async void RefreshDataAfterImageClick(object sender, MouseButtonEventArgs e)
		{
			List<IWeatherService> services = new List<IWeatherService>();
			services.Add(new Openweather());
			services.Add(new Stormglass());

			ResponceReceiver receiver = new ResponceReceiver();
			for (int i = 0; i < services.Count; i++)
			{
				await receiver.GetResponce(services[i].URL);
				WeatherForecast forecast = services[i].GetWeatherForecast(receiver);
				RefreshTextBlockText(designGrid.Children.OfType<Grid>().ToList()[i], forecast);
			}
		}

		private void RefreshTextBlockText(Grid weatherDataGrid, WeatherForecast forecast)
		{
			if (forecast == null)
			{
				foreach (TextBlock textBlock in weatherDataGrid.Children)
					textBlock.Text = "...";

				((TextBlock)weatherDataGrid.Children[6]).Text = "service is not\navailable";

				return;
			}

			forecast.PrepareForUI();

			((TextBlock)weatherDataGrid.Children[0]).Text = forecast.Temperature;
			((TextBlock)weatherDataGrid.Children[1]).Text = forecast.CloudCover;
			((TextBlock)weatherDataGrid.Children[2]).Text = forecast.Humidity;
			((TextBlock)weatherDataGrid.Children[3]).Text = forecast.Precipitation;
			((TextBlock)weatherDataGrid.Children[4]).Text = forecast.WindSpeed;
			((TextBlock)weatherDataGrid.Children[5]).Text = forecast.WindDirection;
			((TextBlock)weatherDataGrid.Children[6]).Text = forecast.SourceSercvice.ToString();
		}
	}
}
