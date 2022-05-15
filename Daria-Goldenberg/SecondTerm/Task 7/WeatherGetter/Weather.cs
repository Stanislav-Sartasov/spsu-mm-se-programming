namespace WeatherGetter
{
	public class Weather
	{
		public readonly double? TemperatureCelsius;
		public readonly double? TemperatureFahrenheit;
		public readonly double? CloudCover;
		public readonly double? Humidity;
		public readonly double? WindSpeed;
		public readonly string? WindDirection;
		public readonly string? Precipitation;

		public Weather(double? temperatureCelsius, double? temperatureFahrenheit, double? cloudCover,
						double? humidity, double? windSpeed, string? windDirection, string? precipitation)
		{
			TemperatureCelsius = temperatureCelsius;
			TemperatureFahrenheit = temperatureFahrenheit;
			CloudCover = cloudCover;
			Humidity = humidity;
			WindSpeed = windSpeed;
			WindDirection = windDirection;
			Precipitation = precipitation;
		}
	}
}