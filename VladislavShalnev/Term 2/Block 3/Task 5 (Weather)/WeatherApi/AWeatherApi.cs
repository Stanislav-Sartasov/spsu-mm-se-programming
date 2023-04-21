using Newtonsoft.Json.Linq;

namespace WeatherApi;

public abstract class AWeatherApi : IWeatherApi
{
	protected abstract string BaseUrl { get; }
	protected abstract string TokenName { get; }
	protected readonly string Token;
	private readonly HttpClient _httpClient;

	protected AWeatherApi(string token)
	{
		Token = token;
		_httpClient = new HttpClient();
		_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
	}

	// For tests only
	protected AWeatherApi(string token, HttpClient httpClient) : this(token) =>
		_httpClient = httpClient;

	protected string BuildRequestUrl(string method, Dictionary<string, string> options)
	{
		string url = $"{BaseUrl}/{method}?";
		url += string.Join("&", options.Select(pair => $"{pair.Key}={pair.Value}"));
		url += $"&{TokenName}={Token}";

		return url;
	}

	protected async Task<JObject> RequestDataAsync(string url)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync(url);
		string responseString = await response.Content.ReadAsStringAsync();

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