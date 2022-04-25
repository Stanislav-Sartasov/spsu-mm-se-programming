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

				ParserOpenWeather parserOpenWeather = new ParserOpenWeather(client);
				ParserTomorrowIo parserTomorrowIo = new ParserTomorrowIo(client);

				Weather weatherOpenWeather = new Weather();
				Weather weatherTomorrowIo = new Weather();

				try
				{
					Console.WriteLine("Forecast from OpenWeather:");
					weatherOpenWeather = await parserOpenWeather.GetWeatherInfoAsync();
					ConsoleOutput.OutputWeather(weatherOpenWeather);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message + "\n");
				}

				try
				{
					Console.WriteLine("Forecast from TomorrowIo:");
					weatherTomorrowIo = await parserTomorrowIo.GetWeatherInfoAsync();
					ConsoleOutput.OutputWeather(weatherTomorrowIo);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message + "\n");
				}

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
	}
}
