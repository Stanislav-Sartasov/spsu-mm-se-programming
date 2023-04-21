using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using OpenWeatherMapApi.Exceptions;
using OpenWeatherMapApi.Models.Weather;
using Requests;
using WeatherApi;

[assembly: InternalsVisibleTo("OpenWeatherMapApi.UnitTests")]
namespace OpenWeatherMapApi;

public class OpenWeatherMap : AWeatherApi
{
	protected override string BaseUrl => "https://api.openweathermap.org/data/2.5";
	protected override string TokenName => "appid";

	public OpenWeatherMap(string token) : base(token) {}

	// For tests only
	internal OpenWeatherMap(string token, IRequestsClient client) : base(token, client) {}

	public override Task<Weather> GetCurrentAsync((double lat, double lon) location) =>
		GetCurrentAsync(location);
	
	public async Task<Weather> GetCurrentAsync(
		(double lat, double lon) location,
		string units = "metric",
		string lang = "en"
	)
	{
		var options = new Dictionary<string, string>
		{
			["lat"] = location.lat.ToString(CultureInfo.InvariantCulture),
			["lon"] = location.lon.ToString(CultureInfo.InvariantCulture),
			["units"] = units,
			["lang"] = lang
		};

		JToken weatherJson = await CallMethodAsync("weather", options);
		OpenWeatherModel? weather = weatherJson.ToObject<OpenWeatherModel>();
		return weather?.ToWeather() ?? new();
	}
	
	protected override void HandleErrors(JObject responseJson)
	{
		if ((int?)responseJson["cod"] != 200)
			throw new ApiException((int)responseJson["cod"]!, (string)responseJson["message"]!);
	}
}