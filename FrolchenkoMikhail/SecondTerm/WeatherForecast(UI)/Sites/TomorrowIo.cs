using Request;
using ConsoleOutput;
using Weather;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sites
{
	public class TomorrowIo : ISites
	{
		public bool active;
		readonly private string address = "https://api.tomorrow.io/v4/timelines?" +
			"location=59.875957,29.829619&" +
			"fields=temperature&" +
			"fields=windSpeed&" +
			"fields=cloudCover&" +
			"fields=windDirection&" +
			"fields=precipitationIntensity&" +
			"fields=humidity&" +
			"timesteps=current&" +
			"apikey=SBpvOKHq9xdJPZ6G896fIEMzxFgASjoG";

		public TomorrowIo(string address)
		{
			this.address = address;
		}

		public TomorrowIo()
		{
		}

		public WeatherData Parse()
		{
			GetRequest request = new (address);
			request.Run();
			if (request.Response == null)
				active = false;
			var response = request.Response;
			var json = JObject.Parse(response);
			IList<JToken> results = json["data"]["timelines"].Children()["intervals"].Children()["values"].Children().ToList();
			string cloudCover = (string)results[0];
			string humidity = (string)results[1];
			string precipitationIntensity = (string)results[2];
			string temp = (string)results[3];
			temp = temp.Replace(".", ",");
			string windDirection = (string)results[4];
			string windSpeed = (string)results[5];
			double tempC = Convert.ToDouble(temp);
			double tempF = tempC * 1.8 + 32;
			return new WeatherData(tempC, tempF, cloudCover, humidity, precipitationIntensity, windDirection, windSpeed);
		}

		public string ShowWeather()
		{
			try
			{
				Output display = new();
				return display.DisplayTheWeather(Parse());
			}
			catch (Exception)
			{
				active = false;
				return "Inactive";
			}
			
		}
	}
}
