using System.Net;

namespace Weather
{
	public class OpenWeatherMapParser : IWeatherParser
	{
		private string _apiKey = "";
		private IWebParser _webParser;

		public OpenWeatherMapParser(IWebParser webParser)
		{
			_webParser = webParser;
		}
		public WeatherData CollectData()
		{
			string dataJson = _webParser.GetData("https://api.openweathermap.org/data/2.5/weather?q=Saint%20Petersburg,%20RU&id=524901&appid=" + _apiKey);

			WeatherData weatherData = FillWeatherData(dataJson);

			if (weatherData.IsNotEmpty())
				return weatherData;
			else
				throw new EmptyWeatherDataException("openweathermap.org gave bad responce and weather data is not filled properly");
		}

		private WeatherData FillWeatherData(string json)
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
					tempCelsius = (Convert.ToSingle(line.Split(":").Last().Replace(".", ",")) - 273.15).ToString("n2");
					tempFahrenheit = (1.8 * (Convert.ToSingle(line.Split(":").Last().Replace(".", ",")) - 273.15) + 32).ToString("n2");
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
