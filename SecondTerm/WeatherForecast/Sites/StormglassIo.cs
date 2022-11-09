using System;
using Weather;
using Request;
using Newtonsoft.Json.Linq;
using ConsoleOutput;

namespace Sites
{
	public class StormglassIo : ISites
	{
		private string address = "https://api.stormglass.io/v2/weather/point?" +
				"lat=59.87595" +
				"&lng=29.82961" +
				"&params=windDirection,windSpeed,cloudCover,humidity,airTemperature,precipitation" +
				"&source=sg" +
				$"&start={DateTimeOffset.Now.ToUnixTimeSeconds()}" +
				$"&end={DateTimeOffset.Now.ToUnixTimeSeconds()}" +
				"&key=e38d51c4-5d45-11ed-a654-0242ac130002-e38d5228-5d45-11ed-a654-0242ac130002";
		
		public StormglassIo(string address)
		{
			this.address = address;
		}

		public StormglassIo()
		{
		}

		public WeatherData Parse()
		{
			GetRequest request = new(address);
			request.Run();
			var response = request.Response;
			var json = JToken.Parse(response)["hours"];
			string cloudCover = (string)json[0]["cloudCover"]["sg"];
			string humidity = (string)json[0]["humidity"]["sg"];
			string precipitationIntensity = (string)json[0]["precipitation"]["sg"];
			string temp = (string)json[0]["airTemperature"]["sg"];
			temp = temp.Replace(".", ",");
			string windDirection = (string)json[0]["windDirection"]["sg"];
			string windSpeed = (string)json[0]["windSpeed"]["sg"];
			double tempC = Math.Round(Convert.ToDouble(temp), 2);
			double tempF = Math.Round(tempC * 1.8 + 32, 2);
			return new WeatherData(tempC, tempF, cloudCover, humidity, precipitationIntensity, windDirection, windSpeed);
		}
		public void ShowWeather()
		{
			Output display = new();
			display.DisplayTheWeather(Parse());
		}
	}
}
