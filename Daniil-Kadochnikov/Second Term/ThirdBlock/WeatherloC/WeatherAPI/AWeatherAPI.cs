using WeatherForecastModel;

namespace WeatherAPI
{
	public abstract class AWeatherAPI : IWeatherAPI
	{
		public string Name { get; }
		public string URL { get; }
		public static HttpClient Client { get; }
		public bool Flag { get; private protected set; } = true;

		internal AWeatherAPI(string url, string name)
		{
			URL = url;
			Name = name;
		}

		static AWeatherAPI() => Client = new HttpClient();

		public abstract Task<string> GetDataAsync();

		public abstract WeatherModel GetWeatherModelAsync(string responseString);
	}
}