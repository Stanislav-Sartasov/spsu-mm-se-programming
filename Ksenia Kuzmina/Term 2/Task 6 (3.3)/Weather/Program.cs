using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Parsers;

namespace Weather
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("This program displays current weather in Saint Petersburg.\n");

			bool check = true;
			bool tomorrowIo = false;
			bool openWeather = false;

			List<IParser> parsers = new List<IParser>();

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == "TomorrowIo" && !tomorrowIo)
				{
					parsers.Add(Container.CreateParser("TomorrowIo"));
					tomorrowIo = true;
				}
				else if (args[i] == "OpenWeather" && !openWeather)
				{
					parsers.Add(Container.CreateParser("OpenWeather"));
					openWeather = true;
				}
				else
				{
					ConsoleOutput.WriteArgsMessage(i.ToString());
				}
			}

			while (check)
			{
				foreach (IParser parser in parsers)
				{
					await GetWeather(parser);
				}

				ConsoleOutput.OutputExitMessage();
				check = UserInput();
			}
		}

		private static bool UserInput()
		{
			if (Console.ReadLine() == "")
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static async Task GetWeather(IParser parser)
		{
			try
			{
				var weather = await parser.GetWeatherInfoAsync();
				ConsoleOutput.OutputSiteName(parser);
				ConsoleOutput.OutputWeather(weather);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\n");
			}
		}
	}
}
