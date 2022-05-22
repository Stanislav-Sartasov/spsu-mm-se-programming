using WeatherForecastModel;

namespace WeatherAPI
{
	public abstract class AWeatherAPI : IWeatherAPI
	{
		public string URL { get; }
		public static HttpClient Client { get; }
		public bool flag { get; private protected set; } = true;

		internal AWeatherAPI(string url) => URL = url;

		static AWeatherAPI() => Client = new HttpClient();

		public abstract Task<string> GetDataAsync();

		public abstract WeatherModel GetWeatherModelAsync(string responseString);
	}
}