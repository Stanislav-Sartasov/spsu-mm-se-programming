using Newtonsoft.Json.Linq;
using Requests;

namespace WeatherApi;

public abstract class AWeatherApi : IWeatherApi
{
	protected abstract string BaseUrl { get; }
	protected abstract string TokenName { get; }
	protected readonly string Token;
	private readonly IRequestsClient _client;

	protected AWeatherApi(string token)
	{
		Token = token;
		_client = new RequestsClient();
	}

	// For tests only
	protected AWeatherApi(string token, IRequestsClient client) : this(token) =>
		_client = client;

	protected string BuildRequestUrl(string method, Dictionary<string, string> options)
	{
		string url = $"{BaseUrl}/{method}?";
		url += string.Join("&", options.Select(pair => $"{pair.Key}={pair.Value}"));
		url += $"&{TokenName}={Token}";

		return url;
	}

	protected async Task<JObject> RequestDataAsync(string url)
	{
		string responseString = await _client.GetStringAsync(url);

		return JObject.Parse(responseString);
	}

	public async Task<JObject> CallMethodAsync(string method, Dictionary<string, string> options)
	{
		string url = BuildRequestUrl(method, options);
		JObject responseJson = await RequestDataAsync(url);

		HandleErrors(responseJson);

		return responseJson;
	}

	protected abstract void HandleErrors(JObject responseJson);
	
	public abstract Task<Weather> GetCurrentAsync((double lat, double lon) location);
}