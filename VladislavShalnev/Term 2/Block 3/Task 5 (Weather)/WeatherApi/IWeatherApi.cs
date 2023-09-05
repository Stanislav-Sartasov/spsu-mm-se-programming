using Newtonsoft.Json.Linq;

namespace WeatherApi;

public interface IWeatherApi
{
	public Task<JObject> CallMethodAsync(string method, Dictionary<string, string> options);

	public Task<Weather> GetCurrentAsync((double lat, double lon) location);
}