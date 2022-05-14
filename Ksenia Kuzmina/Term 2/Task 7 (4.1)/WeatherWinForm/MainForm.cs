using Weather;
using Weather.Parsers;


namespace WeatherWinForm
{
	public partial class MainForm : Form
	{
		private Weather.Weather? _weatherOpenWeather;
		private Weather.Weather? _weatherTomorrowIo;

		public MainForm()
		{
			InitializeComponent();

			FormBorderStyle = FormBorderStyle.FixedSingle;
		}

		private void ButtonExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private async void ButtonUpdateClick(object sender, EventArgs e)
		{
			LoadWeather();
		}

		private async void LoadWeather()
		{
			MyHttpClient client = new MyHttpClient();

			IParser parserTomorrowIo = new ParserTomorrowIo(client);
			IParser parserOpenWeather = new ParserOpenWeather(client);

			_weatherTomorrowIo = await GetWeather(parserTomorrowIo);
			_weatherOpenWeather = await GetWeather(parserOpenWeather);

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

			UpdatePanelData(rightPanel, _weatherTomorrowIo);

			UpdatePanelData(leftPanel, _weatherOpenWeather);

		}

		private void UpdatePanelData(Panel panel, Weather.Weather weatherResult)
		{
			if (weatherResult == null)
			{
				for (int i = 0; i < 7; i++)
					panel.Controls[1 + i].Text = "No data";
				return;
			}

			panel.Controls[1].Text = weatherResult.CelsiusTemperature.ToString() + "°";
			panel.Controls[2].Text = weatherResult.FahrenheitTemperature.ToString() + "°";
			panel.Controls[3].Text = weatherResult.CloudCover.ToString() + "%";
			panel.Controls[4].Text = weatherResult.Precipitation.ToString();
			panel.Controls[5].Text = weatherResult.Humidity.ToString() + "%";
			panel.Controls[6].Text = weatherResult.WindDirection.ToString();
			panel.Controls[7].Text = weatherResult.WindSpeed.ToString() + "m/s";
		}

		private static async Task<Weather.Weather> GetWeather(IParser parser)
		{
			Weather.Weather weather;

			try
			{
				weather = await parser.GetWeatherInfoAsync();
			}
			catch
			{
				weather = null;
			}

			return weather;
		}

		private async void MainFormLoad(object sender, EventArgs e)
		{
			LoadWeather();
		}
	}
}