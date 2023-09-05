using Microsoft.Extensions.Configuration;
using OpenWeatherMapApi;
using TomorrowIoApi;
using WeatherApi;

namespace WeatherWinForms;

public partial class MainForm : Form
{
	private TomorrowIo _tomorrowIo;
	private OpenWeatherMap _openWeatherMap;

	public MainForm()
	{
		InitializeComponent();

		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		_tomorrowIo = new TomorrowIo(config["TomorrowIoToken"]!);
		_openWeatherMap = new OpenWeatherMap(config["OpenWeatherMapToken"]!);

		RefreshData();
	}

	private async void RefreshData()
	{
		tomorrowIoLabel.Text = "Загрузка...";
		openWeatherMapLabel.Text = "Загрузка...";

		(double lat, double lon) location = (59.939098, 30.315868);

		tomorrowIoLabel.Text = await GetStringData(
			_tomorrowIo.GetCurrentAsync(location)
		);

		openWeatherMapLabel.Text = await GetStringData(
			_openWeatherMap.GetCurrentAsync(location, lang: "ru")
		);
	}

	private async Task<string> GetStringData(Task<Weather> task)
	{
		try
		{
			return WeatherConverter.WeatherToString(await task);
		}
		catch (WeatherApi.Exceptions.ApiException ex)
		{
			return "Ошибка: " + ex.Message;
		}
		catch (Exception)
		{
			return "Сервис недоступен.";
		}
	}

	private void OnClick(object sender, EventArgs e) =>
		RefreshData();
}
