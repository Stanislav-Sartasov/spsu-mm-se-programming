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
				$"Precipitation intensity: {weather.PrecipitationIntensity}mm/hr\n" +
				$"Wind direction: {weather.WindDirection}°\n" +
				$"Wind speed: {weather.WindSpeed}m/s";
		}
	}
}
