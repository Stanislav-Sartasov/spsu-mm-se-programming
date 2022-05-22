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
			WeatherModel? weatherTomorrow;
			WeatherModel? weatherSormGlass;

			do
			{
				try
				{
					weatherTomorrow = tomorrowWeatherAPI.GetWeatherModelAsync(await tomorrowWeatherAPI.GetDataAsync());

					if (tomorrowWeatherAPI.flag)
						ConsoleWriter.WriteWeatherForecast(weatherTomorrow, "Tomorrow.io");
					else
						ConsoleWriter.WriteErrorTomorrow();
				}
				catch (Exception e)
				{
					ConsoleWriter.WriteException(e.Message);
				}

				try
				{
					weatherSormGlass = stormGlassWeatherAPI.GetWeatherModelAsync(await stormGlassWeatherAPI.GetDataAsync());

					if (stormGlassWeatherAPI.flag)
						ConsoleWriter.WriteWeatherForecast(weatherSormGlass, "StormGlass.io");
					else
						ConsoleWriter.WriteErrorStormGlass();
				}
				catch (Exception e)
				{
					ConsoleWriter.WriteException(e.Message);
				}

			} while (CheckKeybord());
		}

		private static bool CheckKeybord()
		{
			string trash;
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