namespace WeatherForecastModel
{
	public record class WeatherModel
	{
		public string? CloudCover;
		public string? Humidity;
		public string? PrecipitationProbability;
		public string? Temperature;
		public string? WindDirection;
		public string? WindSpeed;

		public string GetString()
		{
			return "Cloud Cover: " + CloudCover +
				"\nHumidity: " + Humidity +
				"\nPrecipitation Probability: " + PrecipitationProbability +
				"\nTemperature " + Temperature +
				"\nWind Direction " + WindDirection +
				"\nWind Speed " + WindSpeed;
		}
	}
}