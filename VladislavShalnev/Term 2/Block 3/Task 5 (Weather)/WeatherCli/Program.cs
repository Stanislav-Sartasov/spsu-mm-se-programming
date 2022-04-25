using TomorrowIoApi;
using OpenWeatherMapApi;
using Microsoft.Extensions.Configuration;

namespace WeatherCli;

public class Program
{
	public static async Task Main(string[] args)
	{
		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		(double lat, double lon) location = (59.939098, 30.315868);

		var tomorrowIo = new TomorrowIo(config["TomorrowIoToken"]!);
		var openWeatherMap = new OpenWeatherMap(config["OpenWeatherMapToken"]!);

		do
		{
			var tomorrowIoTask = tomorrowIo.GetCurrentAsync(location);
			var openWeatherMapTask = openWeatherMap.GetCurrentAsync(location, lang: "ru");
			
			Console.Clear();
			Console.WriteLine("Текущая погода в Санкт-Петербурге\n");
			
			try
			{
				var tomorrowIoWeather = await tomorrowIoTask;
				Console.WriteLine("--Tomorrow.io--");
				Console.WriteLine(WeatherConverter.WeatherToString(tomorrowIoWeather));
			}
			catch (TomorrowIoApi.Exceptions.ApiException ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch (Exception)
			{
				Console.WriteLine("Сервис Tomorrow.io недоступен.");
			}
			
			Console.WriteLine();
			
			try
			{
				var openWeatherMapWeather = await openWeatherMapTask;
				Console.WriteLine("--OpenWeatherMap.org--");
				Console.WriteLine(WeatherConverter.WeatherToString(openWeatherMapWeather));
			}
			catch (OpenWeatherMapApi.Exceptions.ApiException ex)
			{
				Console.WriteLine("Ошибка: " + ex.Message);
			}
			catch (Exception)
			{
				Console.WriteLine("Сервис OpenWeatherMap.org недоступен.");
			}
			
			Console.WriteLine("\nНажмите любую клавишу, чтобы обновить данные или ESC для выхода.");

		} while (Console.ReadKey().Key != ConsoleKey.Escape);
	}
}