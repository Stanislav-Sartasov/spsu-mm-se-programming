using ConsoleOutput;
using Newtonsoft.Json.Linq;
using Request;
using System;
using Weather;

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
				"&key=0f7198d4-5e1a-11ed-a654-0242ac130002-0f719a00-5e1a-11ed-a654-0242ac130002";

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
