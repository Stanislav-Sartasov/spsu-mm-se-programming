using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WebServicesLib
{
	public class Stormglass : IWebService
	{
		private readonly HttpClient Client;
		private readonly string Latitude;
		private readonly string Longitude;
		private const string Parameters = "airTemperature,cloudCover,humidity,precipitation,windWaveDirection,windSpeed";
		private readonly string Key;
		private string Responce;

		public Stormglass(string latitude, string longitude, string key)
		{
			Client = new HttpClient();
			Latitude = latitude;
			Longitude = longitude;
			Key = key;
		}

		private async Task GetResponce()
		{
			DateTime now = DateTime.UtcNow;
			string start = now.ToString("yyyy-MM-ddTHH:mm:ss");

			try
			{
				Responce = await Client.GetStringAsync($"https://api.stormglass.io/v2/weather/point?lat={Latitude}&lng={Longitude}&params={Parameters}&start={start}&end={start}&key={Key}");
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("Exception Caught!");
				Console.WriteLine("Message: {0}", e.Message);
			}
		}

		public bool PrintResponce()
		{
			var task = GetResponce();
			Task.WaitAll(task);

			if (Responce == null)
			{
				Console.WriteLine("Something went wrong during receiving a response from stormglass.io");
				return false;
			}

			var json = JObject.Parse(Responce);

			Console.WriteLine("According to German Weather Service actual weather is:");
			Console.WriteLine
				(
				"Air temperature: {0}, {1}\n" +
				"Cloud cover in %: {2}\n" +
				"Humidity in %: {3}\n" +
				"Precipitation in mm: {4}\n" +
				"Wind speed in m/s: {5}\n" +
				"Wind wave direction in \u00B0: {6}\n",
				json["hours"][0]["airTemperature"]["dwd"] != null ? json["hours"][0]["airTemperature"]["dwd"] + "\u00B0С" : "no data on this service", json["hours"][0]["airTemperature"]["dwd"] != null ? (1.8 * Convert.ToDouble(json["hours"][0]["airTemperature"]["dwd"]) + 32) + "\u00B0F" : "no data on this service",
				json["hours"][0]["cloudCover"]["dwd"] != null ? json["hours"][0]["cloudCover"]["dwd"] : "no data on this service",
				json["hours"][0]["humidity"]["dwd"] != null ? json["hours"][0]["humidity"]["dwd"] : "no data on this service",
				json["hours"][0]["precipitation"]["dwd"] != null ? json["hours"][0]["precipitation"]["dwd"] : "no data on this service",
				json["hours"][0]["windSpeed"]["icon"] != null ? json["hours"][0]["windSpeed"]["icon"] : "no data on this service",
				json["hours"][0]["windWaveDirection"]["dwd"] != null ? json["hours"][0]["windWaveDirection"]["dwd"] : "no data on this service"
				);

			return true;
		}
	}
}