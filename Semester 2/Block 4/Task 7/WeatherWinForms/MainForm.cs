using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeatherLib.Parsers;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;

namespace WeatherWinForms
{
	public partial class MainForm : Form
	{
		private List<Label> openweatherLabels;
		private List<Label> stormglassLabels;

		public MainForm()
		{
			InitializeComponent();

			openweatherLabels = new List<Label>();
			stormglassLabels = new List<Label>();
			foreach (GroupBox groupBox in Controls.OfType<GroupBox>())
			{
				openweatherLabels.Add(groupBox.Controls.OfType<Label>().ToList()[1]);
				stormglassLabels.Add(groupBox.Controls.OfType<Label>().ToList()[0]);
			}
			openweatherLabels.Add(cellarLeftLabel);
			stormglassLabels.Add(cellarRightLabel);
		}

		private async void RefreshDataAfterPictureBoxClick(object sender, EventArgs e)
		{
			IWeatherService openweather = new Openweather();
			IWeatherService stormglass = new Stormglass();

			ResponceReceiver receiver = new ResponceReceiver();

			await receiver.GetResponce(openweather.URL);
			WeatherForecast oForecast = openweather.GetWeatherForecast(receiver);

			await receiver.GetResponce(stormglass.URL);
			WeatherForecast sForecast = stormglass.GetWeatherForecast(receiver);

			RefreshLabelsText(openweatherLabels, oForecast);
			RefreshLabelsText(stormglassLabels, sForecast);
		}

		private void RefreshLabelsText(List<Label> weatherLabels, WeatherForecast forecast)
		{
			if (forecast == null)
			{
				foreach (Label label in weatherLabels)
					label.Text = "...";
				
				weatherLabels[6].Text = "service is not\navailable";

				return;
			}

			forecast.PrepareForUI();

			weatherLabels[0].Text = forecast.WindDirection;
			weatherLabels[1].Text = forecast.WindSpeed;
			weatherLabels[2].Text = forecast.Precipitation;
			weatherLabels[3].Text = forecast.Humidity;
			weatherLabels[4].Text = forecast.CloudCover;
			weatherLabels[5].Text = forecast.Temperature;
			weatherLabels[6].Text = forecast.SourceSercvice.ToString();
		}
	}
}
