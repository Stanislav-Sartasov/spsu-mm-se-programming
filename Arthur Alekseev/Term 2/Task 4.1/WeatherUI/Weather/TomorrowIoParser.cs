using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUI.Weather
{
	public class TomorrowIoParser : WeatherParser
	{
		public TomorrowIoParser(IWebParser webParser) : base(webParser)
		{
			_apiKey = "";
			_url = "https://api.tomorrow.io/v4/timelines?&timesteps=current&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&location=59.791891,30.264067&apikey=";
		}

		protected override WeatherData FillWeatherData(string json)
		{
			string? tempCelsius = null;
			string? tempFahrenheit = null;
			string? humidity = null;
			string? windSpeed = null;
			string? windDirection = null;
			string? cloudCoverage = null;
			string? downfall = null;


			string[] dataJsonSplitted = json.Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "").Split(",");

			foreach (var line in dataJsonSplitted)
			{
				if (line.Contains("\"temperature\""))
				{
					tempCelsius = (Convert.ToSingle(line.Split(":").Last(), CultureInfo.InvariantCulture)).ToString("n2");
					tempFahrenheit = (1.8 * Convert.ToSingle(line.Split(":").Last(), CultureInfo.InvariantCulture) + 32).ToString("n2");
				}
				if (line.Contains("\"humidity\""))
					humidity = line.Split(":").Last();
				if (line.Contains("\"windSpeed\""))
					windSpeed = line.Split(":").Last();
				if (line.Contains("\"windDirection\""))
					windDirection = line.Split(":").Last();
				if (line.Contains("\"cloudCover\""))
					cloudCoverage = line.Split(":").Last();
				if (line.Contains("\"precipitationType\""))
				{
					var precipitationType = line.Split(":").Last().Replace("\"", "");
					downfall = getPrecipitationType(precipitationType);
				}
			}

			return new WeatherData(tempFahrenheit, tempCelsius, cloudCoverage, humidity, downfall, windDirection, windSpeed, "tomorrow.io");
		}

		private string getPrecipitationType(string precipitationId)
		{
			switch (precipitationId)
			{
				case "0":
					return "Clear";
				case "1":
					return "Rain";
				case "2":
					return "Snow";
				case "3":
					return "Freezing Rain";
				case "4":
					return "Ice Pellets / Sleet";
			}

			return "No Data";
		}
	}
}
