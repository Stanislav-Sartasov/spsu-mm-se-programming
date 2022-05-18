using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using OpenWeatherMapApi;
using TomorrowIoApi;
using WeatherApi;
using WeatherWpf.Models;

namespace WeatherWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public static readonly DependencyProperty TomorrowIoWeatherProperty;
	public static readonly DependencyProperty OpenWeatherMapWeatherProperty;
	public static readonly DependencyProperty CanRefreshProperty;

	static MainWindow()
	{
		TomorrowIoWeatherProperty = DependencyProperty.Register(
			"TomorrowIoWeather",
			typeof(WeatherModel),
			typeof(MainWindow)
		);
		OpenWeatherMapWeatherProperty = DependencyProperty.Register(
			"OpenWeatherMapWeather",
			typeof(WeatherModel),
			typeof(MainWindow)
		);
		CanRefreshProperty = DependencyProperty.Register(
			"CanRefresh",
			typeof(bool),
			typeof(MainWindow),
			new FrameworkPropertyMetadata(true)
		);
	}

	public WeatherModel? TomorrowIoWeather
	{
		get => (WeatherModel?)GetValue(TomorrowIoWeatherProperty);
		set => SetValue(TomorrowIoWeatherProperty, value);
	}
	public WeatherModel? OpenWeatherMapWeather
	{
		get => (WeatherModel?)GetValue(OpenWeatherMapWeatherProperty);
		set => SetValue(OpenWeatherMapWeatherProperty, value);
	}

	public bool CanRefresh
	{
		get => (bool) GetValue(CanRefreshProperty);
		set => SetValue(CanRefreshProperty, value);
	}
	
	private readonly TomorrowIo _tomorrowIo;
	private readonly OpenWeatherMap _openWeatherMap;
		
	public MainWindow()
	{
		InitializeComponent();

		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		_tomorrowIo = new TomorrowIo(config["TomorrowIoToken"]!);
		_openWeatherMap = new OpenWeatherMap(config["OpenWeatherMapToken"]!);
		
		Refresh();
	}

	private void OnClick(object sender, RoutedEventArgs e) =>
		Refresh();

	private async void Refresh()
	{
		CanRefresh = false;
		
		TomorrowIoWeather = new WeatherModel { Status = WeatherStatus.Refreshing };
		OpenWeatherMapWeather = new WeatherModel { Status = WeatherStatus.Refreshing };
		
		(double lat, double lon) location = (59.939098, 30.315868);
		
		var tomorrowIoWeatherSetter = async () =>
		{
			TomorrowIoWeather = await GetWeatherModelData(
				_tomorrowIo.GetCurrentAsync(location)
			);
		};

		var openWeatherMapWeatherSetter = async () =>
		{
			OpenWeatherMapWeather = await GetWeatherModelData(
				_openWeatherMap.GetCurrentAsync(location, lang: "ru")
			);
		};

		await Task.WhenAll(tomorrowIoWeatherSetter(), openWeatherMapWeatherSetter());

		CanRefresh = true;
	}
	
	private async Task<WeatherModel> GetWeatherModelData(Task<Weather> task)
	{
		try
		{
			return new WeatherModel(await task);
		}
		catch (WeatherApi.Exceptions.ApiException ex)
		{
			return new WeatherModel { Status = WeatherStatus.Error, Error = ex.Message };
		}
		catch (Exception)
		{
			return new WeatherModel { Status = WeatherStatus.Error, Error = "Сервис недоступен" };
		}
	}
	
}