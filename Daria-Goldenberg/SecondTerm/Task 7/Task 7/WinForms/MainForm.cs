using WeatherGetter;

namespace WinForms
{
	public partial class MainForm : Form
	{
		OpenWeather openWeather = new OpenWeather(new Request());
		TomorrowIo tomorrowIo = new TomorrowIo(new Request());

		public MainForm()
		{
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			CreateLabels(tomorrowIoPanel);
			CreateLabels(openWeatherPanel);
			UpdateData(tomorrowIoPanel, tomorrowIo);
			UpdateData(openWeatherPanel, openWeather);
		}

		private void UpdateButtonClick(object sender, EventArgs e)
		{
			UpdateData(tomorrowIoPanel, tomorrowIo);
			UpdateData(openWeatherPanel, openWeather);
		}

		private void UpdateData(Panel panel, Site site)
		{
			string[] dataFromSite = new string[5];

			for (int i = 0; i < 5; i++)
			{
				panel.Controls[i].Text = "";
			}

			try
			{
				dataFromSite = GetData(site.GetData());
				for (int i = 0; i < 5; i++)
					panel.Controls[i].Text = dataFromSite[i];
			}
			catch (Exception ex)
			{
				tomorrowIoPanel.Controls[0].Text = ex.Message;
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

		private void ExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void CreateLabels(Panel location)
		{
			for (int i = 0; i < 5; i++)
			{
				var label = new Label();
				label.Text = i.ToString();
				label.Location = new Point(10, i * 37);
				label.Size = new Size(350, 30);
				location.Controls.Add(label);
			}
		}
	}
}