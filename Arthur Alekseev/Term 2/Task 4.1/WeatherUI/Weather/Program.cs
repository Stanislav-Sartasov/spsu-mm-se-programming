using System.Collections;

namespace WeatherUI.Weather
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var tomorrowIoParser = new TomorrowIoParser(WebParser.GetInstance());
			var openWeatherMapParser = new OpenWeatherMapParser(WebParser.GetInstance());

			do
			{
				Console.Clear();
				Console.WriteLine("This program shows weather for today (" + DateTime.Now.ToShortDateString() + ")\n");

				try
				{
					Console.WriteLine(tomorrowIoParser.CollectData());
				}
				catch (Exception ex)
				{
					if (ex is EmptyWeatherDataException)
						Console.WriteLine(ex.Message);
					else
						Console.WriteLine("tomorrow.io is unreachable");
				}

				Console.WriteLine();

				try
				{
					Console.WriteLine(openWeatherMapParser.CollectData());
				}
				catch (Exception ex)
				{
					if (ex is EmptyWeatherDataException)
						Console.WriteLine(ex.Message);
					else
						Console.WriteLine("openweathermap.org is unreachable");
				}

				Console.WriteLine("\nPress Any key to refresh values, or ESC to exit the app");
			}
			while (Console.ReadKey().Key != ConsoleKey.Escape);
		}
	}
}