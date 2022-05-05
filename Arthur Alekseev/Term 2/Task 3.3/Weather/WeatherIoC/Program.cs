using System.Collections;

namespace WeatherIoC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IoCContainer ioCContainer = new IoCContainer();

			bool generateTomorrowIo = args.Contains("tomorrow.io");
			bool generateOpenWeatherMap = args.Contains("openweathermap.org");

			List<IWeatherParser> parsers = ioCContainer.CreateParsers(generateOpenWeatherMap, generateTomorrowIo);

			do
			{
				Console.Clear();
				Console.WriteLine("This program shows weather for today (" + DateTime.Now.ToShortDateString() + ")\n" +
					"Also services can be enabled with command line arguments.\n" +
					"To enable tomorrow.io, use tomorrow.io\n" +
					"To enable openweathermap.oeg, type openweathermap.org\n");

				PrintData(parsers.ToArray());

				Console.WriteLine("\nPress Any key to refresh values, or ESC to exit the app");
			}
			while (Console.ReadKey().Key != ConsoleKey.Escape);
		}

		private static void PrintData(IWeatherParser[] parsers)
		{
			foreach (var parser in parsers)
			{
				try
				{
					Console.WriteLine(parser.CollectData());

				}
				catch (Exception ex)
				{
					if (ex is EmptyWeatherDataException)
						Console.WriteLine(ex.Message);
					else
						Console.WriteLine(parser.Name + " is unreachable");
				}
				Console.WriteLine();
			}
		}
	}
}