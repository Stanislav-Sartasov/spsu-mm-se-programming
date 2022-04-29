using IoC;
using TomorrowIoApi;
using OpenWeatherMapApi;
using Microsoft.Extensions.Configuration;
using WeatherApi;
using WeatherApi.Exceptions;

namespace WeatherCli;

public class Program
{
	public static async Task Main(string[] args)
	{
		var config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		(double lat, double lon) location = (59.939098, 30.315868);
		
		var serviceController = new ServiceController()
			.AddService<TomorrowIo>(config["TomorrowIoToken"]!)
			.AddService<OpenWeatherMap>(config["OpenWeatherMapToken"]!);

		// Demonstration of service removal
		try
		{
			foreach (string serviceName in args)
			{
				switch (serviceName)
				{
					case "-TomorrowIo":
						serviceController.RemoveService<TomorrowIo>();
						break;
					case "-OpenWeatherMap":
						serviceController.RemoveService<OpenWeatherMap>();
						break;
					default:
						throw new ArgumentException($"{serviceName}. Неизвестный сервис. Используйте доступные имена.");
				}
			}
		}
		catch (ArgumentException ex)
		{
			Console.WriteLine("Ошибка: " + ex.Message);
			return;
		}

		do
		{
			Console.Clear();
			Console.WriteLine("Используйте \"-[service name]\" в параметрах запуска для отключения нужного сервиса");
			Console.WriteLine("Доступные сервисы: TomorrowIo, OpenWeatherMap\n");
			Console.WriteLine("Текущая погода в Санкт-Петербурге\n");
			foreach (AWeatherApi service in serviceController.Services)
			{
				try
				{
					Console.WriteLine($"--{service.Name}--");
					var weather = await service.GetCurrentAsync(location);
					Console.WriteLine(WeatherConverter.WeatherToString(weather));
				}
				catch (ApiException ex)
				{
					Console.WriteLine("Ошибка: " + ex.Message);
				}
				catch (Exception)
				{
					Console.WriteLine("Сервис недоступен.");
				}
				Console.WriteLine();
			}
			Console.WriteLine("Нажмите любую клавишу, чтобы обновить данные или ESC для выхода.");
		} while (Console.ReadKey().Key != ConsoleKey.Escape);
	}
}