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

		public string Name => "TomorrowIo";

		public ParserTomorrowIo(IHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<Weather> GetWeatherInfoAsync()
		{
			string response = await _httpClient.GetData(_url + "");

			JObject responseJson = JObject.Parse(response);


			if (responseJson["code"] != null)
			{
				WriteErrorMessage();
				throw new Exception((string?)responseJson["message"]);
			}

			Weather weather = new Weather(
				(float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["temperature"],
				(9 / 5) * ((float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["temperature"] + 32),
				(string)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["cloudCover"],
				(int)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["humidity"],
				FindOutPrecipation((int)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["precipitationType"]),
				(string)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["windDirection"],
				(float)responseJson["data"]["timelines"][0]["intervals"][0]["values"]["windSpeed"]);

			return weather;
		}

		private void WriteErrorMessage()
		{
			Console.WriteLine("Something went wrong");
		}

		private string FindOutPrecipation(int prec)
		{
			if (prec == 0)
				return "No precipitation";
			else if (prec == 1)
				return "Rain";
			else if (prec == 2)
				return "Snow";
			else
				return "Undefined";
		}
	}
}
