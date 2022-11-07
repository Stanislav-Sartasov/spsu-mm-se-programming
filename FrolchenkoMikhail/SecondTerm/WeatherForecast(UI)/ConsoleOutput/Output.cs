using System;
using Weather;

namespace ConsoleOutput
{
	public class Output
	{
		public string DisplayTheWeather(WeatherData weather)
		{
			return $"Temperature: {weather.TempC}°C\n" +
				$"Temperature: {weather.TempF}°F\n" +
				$"Cloudcover: {weather.Cloudcover}%\n" +
				$"Humidity: {weather.Humidity}%\n" +
				$"PrecipitationIntensity: {weather.PrecipitationIntensity}mm/hr\n" +
				$"WindDirection: {weather.WindDirection}°\n" +
				$"WindSpeed: {weather.WindSpeed}m/s";
		}
	}
}
