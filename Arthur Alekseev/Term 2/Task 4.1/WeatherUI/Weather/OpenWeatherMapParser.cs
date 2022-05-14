using System.Globalization;
using System.Net;

namespace WeatherUI.Weather
{
	public class OpenWeatherMapParser : WeatherParser
	{
		public OpenWeatherMapParser(IWebParser parser) : base(parser)
		{
			_apiKey = "a940f3a7de391a01b03acf028f94f092";
			_url = "https://api.openweathermap.org/data/2.5/weather?q=Saint%20Petersburg,%20RU&id=524901&appid=";
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
				if (line.Contains("\"temp\""))
				{
					tempCelsius = (Convert.ToSingle(line.Split(":").Last(), CultureInfo.InvariantCulture) - 273.15).ToString("n2");
					tempFahrenheit = (1.8 * (Convert.ToSingle(line.Split(":").Last(), CultureInfo.InvariantCulture) - 273.15) + 32).ToString("n2");
				}
				if (line.Contains("\"humidity\""))
					humidity = line.Split(":").Last();
				if (line.Contains("\"speed\""))
					windSpeed = line.Split(":").Last();
				if (line.Contains("\"deg\""))
					windDirection = line.Split(":").Last();
				if (line.Contains("\"clouds\""))
					cloudCoverage = line.Split(":").Last();
				if (line.Contains("\"main\"") && downfall == null)
					downfall = line.Split(":").Last().Replace("\"", "");
			}

			return new WeatherData(tempFahrenheit, tempCelsius, cloudCoverage, humidity, downfall, windDirection, windSpeed, "openweathermap.org");
		}
	}
}
