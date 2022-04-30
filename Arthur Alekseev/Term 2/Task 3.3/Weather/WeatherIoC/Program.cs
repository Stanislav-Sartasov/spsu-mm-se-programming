using System.Collections;

namespace WeatherIoC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IoCContainer ioCContainer = new IoCContainer();
			List<IWeatherParser> parsers = ioCContainer.CreateParsers();

			do
			{
				Console.Clear();
				Console.WriteLine("This program shows weather for today (" + DateTime.Now.ToShortDateString() + ")\n");

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