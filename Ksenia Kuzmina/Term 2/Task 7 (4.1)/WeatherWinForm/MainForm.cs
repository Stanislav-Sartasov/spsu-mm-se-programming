using Weather;
using Weather.Parsers;


namespace WeatherWinForm
{
	public partial class MainForm : Form
	{
		private Weather.Weather _weatherOpenWeather;
		private Weather.Weather _weatherTomorrowIo;

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

			if (_weatherTomorrowIo != null)
			{
				tomorrowIoCelsiusLabel.Text = _weatherTomorrowIo.CelsiusTemperature.ToString() + "°";
				tomorrowIoFahrenheitLabel.Text = _weatherTomorrowIo.FahrenheitTemperature.ToString() + "°";
				tomorrowIoCloudCoverLabel.Text = _weatherTomorrowIo.CloudCover.ToString() + "%";
				tomorrowIoPrecipationLabel.Text = _weatherTomorrowIo.Precipitation.ToString();
				tomorrowIoHumidityLabel.Text = _weatherTomorrowIo.Humidity.ToString() + "%";
				tomorrowIoWindDirectionLabel.Text = _weatherTomorrowIo.WindDirection.ToString();
				tomorrowIoWindSpeedLabel.Text = _weatherTomorrowIo.WindSpeed.ToString() + "m/s";
			}
			else
			{
				tomorrowIoCelsiusLabel.Text = "No data";
				tomorrowIoFahrenheitLabel.Text = "No data";
				tomorrowIoCloudCoverLabel.Text = "No data";
				tomorrowIoPrecipationLabel.Text = "No data";
				tomorrowIoHumidityLabel.Text = "No data";
				tomorrowIoWindDirectionLabel.Text = "No data";
				tomorrowIoWindSpeedLabel.Text = "No data";
			}

			if (_weatherOpenWeather != null)
			{
				openWeatherCelsiusLabel.Text = _weatherOpenWeather.CelsiusTemperature.ToString() + "°";
				openWeatherFahrenheitLabel.Text = _weatherOpenWeather.FahrenheitTemperature.ToString() + "°";
				openWeatherCloudCoverLabel.Text = _weatherOpenWeather.CloudCover.ToString() + "%";
				openWeatherPrecipationLabel.Text = _weatherOpenWeather.Precipitation.ToString();
				openWeatherHumidityLabel.Text = _weatherOpenWeather.Humidity.ToString() + "%";
				openWeatherWindDirectionLabel.Text = _weatherOpenWeather.WindDirection.ToString();
				openWeatherWindSpeedLabel.Text = _weatherOpenWeather.WindSpeed.ToString() + "m/s";
			}
			else
			{
				openWeatherCelsiusLabel.Text = "No data";
				openWeatherFahrenheitLabel.Text = "No data";
				openWeatherCloudCoverLabel.Text = "No data";
				openWeatherPrecipationLabel.Text = "No data";
				openWeatherHumidityLabel.Text = "No data";
				openWeatherWindDirectionLabel.Text = "No data";
				openWeatherWindSpeedLabel.Text = "No data";
			}
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