using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WebServicesLib
{
	public class Openweathermap : IWebService
	{
		private readonly HttpClient Client;
		private readonly string Latitude;
		private readonly string Longitude;
		private const string Units = "metric";
		private readonly string Key;
		private string Responce;

		public Openweathermap(string latitude, string longitude, string key)
		{
			Client = new HttpClient();
			Latitude = latitude;
			Longitude = longitude;
			Key = key;
		}

		private async Task GetResponce()
		{
			try
			{
				Responce = await Client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longitude}&units={Units}&appid={Key}");
			}
			catch (HttpRequestException e)
			{
				Console.WriteLine("\nException Caught!");
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

			Console.WriteLine("According to Open Weather actual weather is:");
			Console.WriteLine
				(
				"Air temperature: {0}, {1}\n" +
				"Cloud cover in %: {2}\n" +
				"Humidity in %: {3}\n" +
				"Precipitation in mm: {4}\n" +
				"Wind speed in m/s: {5}\n" +
				"Wind wave direction in \u00B0: {6}\n",
				json["main"]["temp"] != null ? json["main"]["temp"] + "\u00B0С" : "no data on this service", json["main"]["temp"] != null ? (1.8 * Convert.ToDouble(json["main"]["temp"]) + 32) + "\u00B0F" : "no data on this service",
				json["clouds"]["all"] != null ? json["clouds"]["all"] : "no data on this service",
				json["main"]["humidity"] != null ? json["main"]["humidity"] : "no data on this service",
				"no data on this service",
				json["wind"]["speed"] != null ? json["wind"]["speed"] : "no data on this service",
				json["wind"]["deg"] != null ? json["wind"]["deg"] : "no data on this service"
				);

			return true;
		}
	}
}