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

			tioLabel = new WeatherDataLabel(panel2);
			owmLabel = new WeatherDataLabel(panel3);

			owmParser = new OpenWeatherMapParser(WebParser.Instance);
			tioParser = new TomorrowIoParser(WebParser.Instance);

			RefreshData();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			RefreshData();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

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