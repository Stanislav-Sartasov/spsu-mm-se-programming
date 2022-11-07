using System;
using Weather;

namespace ConsoleOutput
{
	public class Output
	{
		public void DisplayTheWeather(WeatherData weather)
		{
			Console.WriteLine($"Temperature: {weather.TempC}°C");
			Console.WriteLine($"Temperature: {weather.TempF}°F");
			Console.WriteLine($"Cloudcover: {weather.Cloudcover}%");
			Console.WriteLine($"Humidity: {weather.Humidity}%");
			Console.WriteLine($"PrecipitationIntensity: {weather.PrecipitationIntensity}mm/hr");
			Console.WriteLine($"WindDirection: {weather.WindDirection}°");
			Console.WriteLine($"WindSpeed: {weather.WindSpeed}m/s");
		}
	}
}
