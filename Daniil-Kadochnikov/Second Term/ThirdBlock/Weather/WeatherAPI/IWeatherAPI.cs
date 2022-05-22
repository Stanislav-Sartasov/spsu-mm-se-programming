using WeatherForecastModel;
namespace WeatherAPI
{
	public interface IWeatherAPI
	{
		string URL { get; }
		static HttpClient Client { get; }

		Task<string> GetDataAsync();

		WeatherModel GetWeatherModelAsync(string responseString);
	}
}
