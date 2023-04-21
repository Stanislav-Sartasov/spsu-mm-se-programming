using TomorrowIOAPI;
using StormglassIOAPI;
using WebWeatherRequester;
using WeatherRequesterResourceLibrary;

namespace WeatherAppWinForms
{
	public partial class WeatherApp : Form
	{
		private List<Label>[] weatherDataLabels = new List<Label>[2];
		private IWeatherRequester[] weatherSources = new IWeatherRequester[2];
		private GroupBox[] weatherBoxes = new GroupBox[2];
		private int tabIndex = 50;

		public WeatherApp()
		{
			InitializeComponent();

			weatherSources[0] = new WebWeather(new TomorrowIOHandler(TomorrowIOResources.APIKey));
			weatherSources[1] = new WebWeather(new StormglassIOHandler(StormglassIOResources.APIKey));

			weatherBoxes[0] = tomorrowBox;
			weatherBoxes[1] = stormglassBox;

			weatherDataLabels[0] = new List<Label>();
			weatherDataLabels[1] = new List<Label>();

			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					var label = new Label();
					label.AutoSize = true;
					label.Location = j < 7 ? new Point(6, 45 * (j + 1)) : new Point(60, 395);
					label.Name = $"weatherInfoLabel{i}_{j}";
					label.Size = new Size(60, 20);
					label.TabIndex = tabIndex;
					label.Text = "none";
					if (j == 7)
					{
						label.Text = "loading...";
						label.Font = new Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
					}
					weatherDataLabels[i].Add(label);
					weatherBoxes[i].Controls.Add(label);
					tabIndex++;
				}
			}
		}

		private void UpdateButton_Click(object sender, EventArgs e)
		{
			UpdateWeather();
		}

		private void UpdateWeather()
		{
			for (int i = 0; i < weatherSources.Length; i++)
			{
				var weatherData = weatherSources[i].FetchWeatherData();

				if (weatherData == null)
				{
					weatherDataLabels[i][7].Text = "Error. Showing last data";
					weatherDataLabels[i][7].ForeColor = Color.Red;
					continue;
				}
				weatherDataLabels[i][7].Text = "Success";
				weatherDataLabels[i][7].ForeColor = Color.Green;

				weatherDataLabels[i][0].Text = $"{weatherData.TempC:F0}°";
				weatherDataLabels[i][1].Text = $"{weatherData.TempF:F0}°";
				weatherDataLabels[i][2].Text = $"{weatherData.Humidity}%";
				weatherDataLabels[i][3].Text = $"{weatherData.CloudCover}%";
				weatherDataLabels[i][4].Text = $"{weatherData.WindSpeed:F2} m\\s";
				weatherDataLabels[i][5].Text = $"{weatherData.WindDirection}";
				weatherDataLabels[i][6].Text = $"{weatherData.Precipitation}";
			}
		}

		private void WeatherApp_Load(object sender, EventArgs e)
		{
			UpdateWeather();
		}
	}
}