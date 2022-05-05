using System.Text.Json;
using WeatherRequesterResourceLibrary;
using WebWeatherRequester;

namespace StormglassIOAPI
{
	public class StormglassIOHandler : AWebServiceAPI
	{
		private static string StormSource = "sg";

		public StormglassIOHandler(string key) : base(key)
		{
		}

		public override RequestDataContainer GetServiceRequestInfo()
		{
			return new RequestDataContainer
			{
				ServiceURL = "https://api.stormglass.io/v2/weather/point"
					+ "?lat=59.9386"
					+ "&lng=30.3141"
					+ "&params=windDirection,windSpeed,cloudCover,humidity,airTemperature,precipitation"
					+ "&source=sg"
					+ $"&start={DateTimeOffset.Now.ToUnixTimeSeconds()}"
					+ $"&end={DateTimeOffset.Now.ToUnixTimeSeconds()}",
				HeaderKeys = new List<string>() { "Authorization" },
				HeaderValues = new List<string>() { apiKey }
			};
		}

		public override WeatherDataContainer? ParseJSONObject(string json)
		{
			try
			{
				using (JsonDocument doc = JsonDocument.Parse(json))
				{
					var weather = doc.RootElement
						.GetProperty("hours").EnumerateArray().First();

					double temp = weather.GetProperty("airTemperature").GetProperty(StormSource).GetDouble();
					double precipitation = weather.GetProperty("precipitation").GetProperty(StormSource).GetDouble();
					PrecipitationTypes type;

					if (precipitation < 0.005)
						type = PrecipitationTypes.None;
					else if (temp > 0)
						type = PrecipitationTypes.Rain;
					else
						type = PrecipitationTypes.Snow;

					return new WeatherDataContainer
					{
						SourceName = "StormglassIO",

						TempC = temp,
						Humidity = (int)weather.GetProperty("humidity").GetProperty(StormSource).GetDouble(),
						WindSpeed = weather.GetProperty("windSpeed").GetProperty(StormSource).GetDouble(),
						WindDirection = weather.GetProperty("windDirection").GetProperty(StormSource).GetDouble(),
						PrecipitationVolumetric = precipitation,
						Precipitation = type,
						CloudCover = (int)weather.GetProperty("cloudCover").GetProperty(StormSource).GetDouble()
					};
				}
			}
			catch
			{
				return null;
			}
		}
	}
}