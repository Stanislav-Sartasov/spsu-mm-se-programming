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

			while (true)
			{
				MyHttpClient client = new MyHttpClient();

				IParser parserOpenWeather = new ParserOpenWeather(client);
				IParser parserTomorrowIo = new ParserTomorrowIo(client);

				await GetWeather(parserOpenWeather);
				await GetWeather(parserTomorrowIo);

				ConsoleOutput.OutputExitMessage();

				if (Console.ReadLine() == "")
				{
					continue;
				}
				else
				{
					break;
				}
			}
		}

		private static async Task GetWeather(IParser parser)
		{
			try
			{
				var weather = await parser.GetWeatherInfoAsync();
				if (parser.Name == "OpenWeather")
				{
					Console.WriteLine("Forecast from OpenWeather:");
				}
				else if (parser.Name == "TomorrowIo")
				{
					Console.WriteLine("Forecast from TomorrowIo:");
				}
				ConsoleOutput.OutputWeather(weather);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + "\n");
			}
		}
	}
}
