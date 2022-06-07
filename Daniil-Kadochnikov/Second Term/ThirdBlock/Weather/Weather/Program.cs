using WeatherAPI;
using WeatherForecastModel;

namespace Weather
{
	public class Program
	{
		public static async Task Main()
		{
			ConsoleWriter.WriteGreeting();
			AWeatherAPI tomorrowWeatherAPI = new TomorrowAPI();
			AWeatherAPI stormGlassWeatherAPI = new StormGlassAPI();

			do
			{
				await ShowWeatherInfo(tomorrowWeatherAPI);
				await ShowWeatherInfo(stormGlassWeatherAPI);

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