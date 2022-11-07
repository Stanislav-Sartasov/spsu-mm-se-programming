namespace Weather
{
	public class WeatherData
	{
		public double TempC { get; private set; }
		public double TempF { get; private set; }
		public string Cloudcover { get; private set; }
		public string Humidity { get; private set; }
		public string PrecipitationIntensity { get; private set; }
		public string WindDirection { get; private set; }
		public string WindSpeed { get; private set; }

		public WeatherData(double tempC, double tempF, string cloudCover, string humidity, string precipitation, string windDirection, string windSpeed)
		{
			TempC = tempC;
			TempF = tempF;
			Cloudcover = cloudCover;
			Humidity = humidity;
			PrecipitationIntensity = precipitation;
			WindDirection = windDirection;
			WindSpeed = windSpeed;
		}
	}
}


