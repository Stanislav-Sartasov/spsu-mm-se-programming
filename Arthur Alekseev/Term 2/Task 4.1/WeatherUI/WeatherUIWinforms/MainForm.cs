using WeatherUI.Weather;

namespace WeatherUIWinforms
{
	public partial class MainForm : Form
	{
		WeatherDataLabel tioLabel;
		WeatherDataLabel owmLabel;

		TomorrowIoParser tioParser;
		OpenWeatherMapParser owmParser;

		public MainForm()
		{
			InitializeComponent();

			tioLabel = new WeatherDataLabel(tioPanel);
			owmLabel = new WeatherDataLabel(owmPanel);

			owmParser = new OpenWeatherMapParser(WebParser.Instance);
			tioParser = new TomorrowIoParser(WebParser.Instance);

			RefreshData();
		}

		private void ExitButtonClick(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void RefreshButtonClick(object sender, EventArgs e)
		{
			RefreshData();
		}

		private void RefreshData()
		{
			try
			{
				tioLabel.LoadWeatherData(tioParser.CollectData());
			}
			catch
			{
				tioLabel.LoadWeatherData(null);
			}
			try
			{
				owmLabel.LoadWeatherData(owmParser.CollectData());
			}
			catch
			{
				owmLabel.LoadWeatherData(null);
			}
		}
	}
}