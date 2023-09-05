namespace WeatherRequesterResourceLibrary
{
	public interface IWeatherRequester
	{
		public WeatherDataContainer? FetchWeatherData();

		public FetchWeatherLog? GetLastLog();
	}
}