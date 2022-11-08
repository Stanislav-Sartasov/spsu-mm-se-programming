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
		private List<IWeatherService> services;
		private List<List<Label>> dataLabels = new List<List<Label>>();

		public MainForm()
		{
			InitializeComponent();

			services = new List<IWeatherService>();
			services.Add(new Openweather());
			services.Add(new Stormglass());

			foreach (IWeatherService service in services)
			{
				dataLabels.Add(new List<Label>());
			}

			foreach (GroupBox groupBox in Controls.OfType<GroupBox>())
			{
				dataLabels[0].Add(groupBox.Controls.OfType<Label>().ToList()[0]);
				dataLabels[1].Add(groupBox.Controls.OfType<Label>().ToList()[1]);
			}
			dataLabels[0].Add(cellarRightLabel);
			dataLabels[1].Add(cellarLeftLabel);
		}

		private async void RefreshDataAfterPictureBoxClick(object sender, EventArgs e)
		{
			ResponceReceiver receiver = new ResponceReceiver();
			for (int i = 0; i < services.Count; i++)
			{
				await receiver.GetResponce(services[i].URL);
				WeatherForecast forecast = services[i].GetWeatherForecast(receiver);
				RefreshLabelsText(dataLabels[i], forecast);
			}
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
