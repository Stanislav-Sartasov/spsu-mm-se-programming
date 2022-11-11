using WeatherAPI;
using WeatherForecastModel;
using WeatherloC;

namespace Weather
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			ConsoleWriter.WriteGreeting();
			
			bool tomorrowIo = args.Contains("Tomorrow.io");
			bool stormGlassIo = args.Contains("StormGlass.io");
			stormGlassIo = true;
			Container container = new Container(tomorrowIo, stormGlassIo);

			var weatherAPIs = container.GetWeatherAPIs();

			do
			{
				foreach (var weather in weatherAPIs)
					await ShowWeatherInfo(weather);

			} while (CheckKeybord());
		}

		private static async Task ShowWeatherInfo(AWeatherAPI weatherApi)
		{
			WeatherModel? weatherModel;

			try
			{
				weatherModel = weatherApi.GetWeatherModelAsync(await weatherApi.GetDataAsync());

				if (weatherApi.Flag)
					ConsoleWriter.WriteWeatherForecast(weatherModel, weatherApi.Name);
				else
					ConsoleWriter.WriteErrorStormGlass();
			}
			catch (Exception e)
			{
				ConsoleWriter.WriteException(e.Message);
			}
		}

		private static bool CheckKeybord()
		{
			ConsoleWriter.WriteAboutKeybord();
			ConsoleKeyInfo cli;

			while (true)
			{
				cli = Console.ReadKey(true);
				if (cli.Key == ConsoleKey.Escape)
					return false;
				if (cli.Key == ConsoleKey.Enter)
					return true;
			}
		}
	}
}