using Sites;
using System;

namespace WeatherForecast
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("This program shows the weather in Peterhof");
			Console.WriteLine("Press esc to exit the program or another key to update the information");
			Console.WriteLine("Remember, sites have a limited number of requests");

			while (Console.ReadKey().Key != ConsoleKey.Escape)
			{
				Console.Clear();
				Console.WriteLine($"The local time:{DateTime.Now}");
				Console.WriteLine("Weather according to website Tomorrow.io:");
				new TomorrowIo().ShowWeather();
				Console.WriteLine("\nWeather according to website Stormglass.io:");
				new StormglassIo().ShowWeather();
			}
		}
	}
}
