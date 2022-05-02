using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Parsers;

namespace Weather
{
	public static class ConsoleOutput
	{
		public static void OutputWeather(Weather weather)
		{
			if (weather.CelsiusTemperature == null)
			{
				Console.WriteLine("Celsius temperature: no data");
				Console.WriteLine("Fahrenheit temperature: no data");
			}
			else
			{
				Console.WriteLine("Celsius temperature: " + ((float)weather.CelsiusTemperature).ToString("n2") + "°");
				Console.WriteLine("Fahrenheit temperature: " + ((float)weather.FahrenheitTemperature).ToString("n2") + "°");
			}

			if (weather.CloudCover == null)
			{
				Console.WriteLine("Cloud cover: no data");
			}
			else
			{
				Console.WriteLine("Cloud cover: " + weather.CloudCover);
			}

			if (weather.Humidity == null)
			{
				Console.WriteLine("Humidity: no data");
			}
			else
			{
				Console.WriteLine("Humidity: " + weather.Humidity + "%");
			}

			if (weather.Precipitation == null)
			{
				Console.WriteLine("Precipation: no data");
			}
			else
			{
				Console.WriteLine("Precipation: " + weather.Precipitation);
			}

			if (weather.WindDirection == null)
			{
				Console.WriteLine("Wind direction: no data");
			}
			else
			{
				Console.WriteLine("Wind direction: " + weather.WindDirection);
			}

			if (weather.WindSpeed == null)
			{
				Console.WriteLine("Wind speed: no data");
			}
			else
			{
				Console.WriteLine("Wind speed: " + weather.WindSpeed + " m/s");
			}

			Console.WriteLine();
		}

		public static void OutputExitMessage()
		{
			Console.WriteLine("Press enter to update the data.");
		}

		public static void OutputSiteName(IParser parser)
		{
			if (parser.Name == "OpenWeather")
			{
				Console.WriteLine("Forecast from OpenWeather:");
			}
			else if (parser.Name == "TomorrowIo")
			{
				Console.WriteLine("Forecast from TomorrowIo:");
			}
		}

		public static void WriteArgsMessage(string number)
		{
			Console.WriteLine("Argument " + number + " was not recognized or you have already written the name of the service.\n");
		}
	}
}
