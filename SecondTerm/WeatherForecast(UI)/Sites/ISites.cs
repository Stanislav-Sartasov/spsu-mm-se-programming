namespace Sites
{
	public interface ISites
	{
		public string ShowWeather();
		public Weather.WeatherData Parse();
	}
}
