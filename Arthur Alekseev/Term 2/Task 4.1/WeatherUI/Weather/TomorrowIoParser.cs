using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUI.Weather
{
	public class TomorrowIoParser : IWeatherParser
	{
		private string _apiKey = "";
		private IWebParser _webParser;

		public TomorrowIoParser(IWebParser webParser)
		{
			_webParser = webParser;
		}

		public WeatherData CollectData()
		{
			string dataJson = _webParser.GetData("https://api.tomorrow.io/v4/timelines?&timesteps=current&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&location=59.791891,30.264067&apikey=" + _apiKey);
			WeatherData weatherData = fillWeatherData(dataJson);

			if (weatherData.IsNotEmpty())
				return weatherData;
			else
				throw new EmptyWeatherDataException("tomorrow.io gave bad responce and weather data is not filled properly");
		}

		private WeatherData fillWeatherData(string json)
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
					tempCelsius = (Convert.ToSingle(line.Split(":").Last().Replace(".", ","))).ToString("n2");
					tempFahrenheit = (1.8 * Convert.ToSingle(line.Split(":").Last().Replace(".", ",")) + 32).ToString("n2");
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
