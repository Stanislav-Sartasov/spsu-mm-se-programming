using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Weather.Parsers;

namespace Weather
{
	public class ParserOpenWeather : IParser
	{
		private IHttpClient _httpClient;
		private readonly string _url = "https://api.openweathermap.org/data/2.5/weather?lon=30.2642&lat=59.8944&units=metric&appid=";

		public string Name => "OpenWeather";

		public ParserOpenWeather(IHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<Weather> GetWeatherInfoAsync()
		{
			string response = await _httpClient.GetData(_url + "c98b1294c5df3c7bf5b37238ebb37423");

			JObject responseJson = JObject.Parse(response);

			if ((int?)responseJson["cod"] != 200)
			{
				throw new Exception((string?)responseJson["message"]);
			}

			Weather weather = new Weather(
				(float)responseJson["main"]["temp"],
				(9 / 5) * ((float)responseJson["main"]["temp"] + 32),
				(string)responseJson["clouds"]["all"],
				(int)responseJson["main"]["humidity"],
				(string)responseJson["weather"][0]["main"],
				(string)responseJson["wind"]["deg"],
				(int)responseJson["wind"]["speed"]);

			return weather;
		}
	}
}
