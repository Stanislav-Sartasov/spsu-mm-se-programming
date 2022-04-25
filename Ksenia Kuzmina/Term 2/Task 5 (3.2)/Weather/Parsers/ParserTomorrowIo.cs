using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Weather.Parsers;

namespace Weather
{
	public class ParserTomorrowIo : IParser
	{
		private IHttpClient _httpClient;
		private readonly string _url = "https://api.tomorrow.io/v4/timelines?&timesteps=current&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&location=59.8944,30.2642&apikey=";

		public ParserTomorrowIo(IHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<Weather> GetWeatherInfoAsync()
		{
			string response = await _httpClient.GetData(_url + "");

			JObject responseJson = JObject.Parse(response);

			Weather weather = new Weather();

			if (responseJson["code"] != null)
			{
				Console.WriteLine("Something went wrong");
				throw new Exception((string?)responseJson["message"]);
			}

			weather.SetWeather(
				(float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["temperature"],
				(9 / 5) * ((float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["temperature"] + 32),
				(string)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["cloudCover"],
				(int)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["humidity"],
				(string)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["precipitationType"],
				(string)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["windDirection"],
				(float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["windSpeed"]);

			return weather;
		}
	}
}
