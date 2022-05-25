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
			Console.WriteLine("This program displays current weather in Saint Petersburg.");

			bool check = true;

			while (check)
			{
				MyHttpClient client = new MyHttpClient();

				IParser parserOpenWeather = new ParserOpenWeather(client);
				IParser parserTomorrowIo = new ParserTomorrowIo(client);

				await GetWeather(parserOpenWeather);
				await GetWeather(parserTomorrowIo);

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
