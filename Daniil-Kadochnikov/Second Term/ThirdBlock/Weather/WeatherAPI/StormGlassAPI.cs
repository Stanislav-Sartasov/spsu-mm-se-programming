using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using WeatherForecastModel;

namespace WeatherAPI
{
	public class StormGlassAPI : AWeatherAPI
	{
		public StormGlassAPI() : base("https://api.stormglass.io/v2/weather/point?lat=59.93863&lng=30.31413&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed", "StormGlass.io") { }

		public override async Task<string> GetDataAsync()
		{
			try
			{
				Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Keys.StormGlassIoAPIkey);
				HttpResponseMessage responseHttp = await Client.GetAsync(URL + $"&start={new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}&end={new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}&source=sg"); // + $"&start={DateTime.Now}&end={DateTime.Now}&source=sg"
				responseHttp.EnsureSuccessStatusCode();

				return await responseHttp.Content.ReadAsStringAsync();
			}
			catch (Exception e)
			{
				Flag = false;
				throw e;
			}
		}

		public override WeatherModel GetWeatherModelAsync(string responseString)
		{
			JObject json = JObject.Parse(responseString);
			JObject? values = (JObject?)json["hours"][0];
			WeatherModel? model = new WeatherModel();
			model.CloudCover = (string)values["cloudCover"]["sg"];
			model.Humidity = (string)values["humidity"]["sg"];
			model.PrecipitationProbability = (string)values["precipitation"]["sg"];
			model.Temperature = (string)values["airTemperature"]["sg"];
			model.WindDirection = (string)values["windDirection"]["sg"];
			model.WindSpeed = (string)values["windSpeed"]["sg"];
			return model;
		}
	}
}