using System.Text.Json;
using WeatherRequesterResourceLibrary;
using WebWeatherRequester;

namespace TomorrowIOAPI
{
	public class TomorrowIOHandler : AWebServiceAPI
	{
		private static PrecipitationTypes[] PrecipitationId = new PrecipitationTypes[] 
		{ 
			PrecipitationTypes.None,
			PrecipitationTypes.Rain,
			PrecipitationTypes.Snow,
			PrecipitationTypes.FreezingRain,
			PrecipitationTypes.IcePellets
		};

		public TomorrowIOHandler(string key) : base(key)
		{
		}

		public override RequestDataContainer GetServiceRequestInfo()
		{
			return new RequestDataContainer
			{
				ServiceURL = "https://api.tomorrow.io/v4/timelines"
					+ "?location=6267dfab3d014f000787b666"
					+ "&fields=temperature"
					+ "&fields=humidity"
					+ "&fields=windSpeed"
					+ "&fields=windDirection"
					+ "&fields=precipitationProbability"
					+ "&fields=precipitationType"
					+ "&fields=cloudCover"
					+ "&units=metric"
					+ "&timesteps=current"
					+ $"&apikey={apiKey}"
			};
		}
		
		public override WeatherDataContainer? ParseJSONObject(string json)
		{
			try
			{
				using (JsonDocument doc = JsonDocument.Parse(json))
				{
					var weather = doc.RootElement
						.GetProperty("data")
						.GetProperty("timelines").EnumerateArray().First()
						.GetProperty("intervals").EnumerateArray().First()
						.GetProperty("values");

					double tempC = weather.GetProperty("temperature").GetDouble();

					return new WeatherDataContainer
					{
						SourceName = "TomorrowIO",

						TempC = tempC,
						TempF = GeneralConversions.ConvertTempFromCToF(tempC),
						Humidity = (int)weather.GetProperty("humidity").GetDouble(),
						WindSpeed = weather.GetProperty("windSpeed").GetDouble(),
						WindDirection = GeneralConversions.GetDirectionFromAngle(weather.GetProperty("windDirection").GetDouble()),
						Precipitation = PrecipitationId[weather.GetProperty("precipitationType").GetInt32()],
						PrecipitationProbability = weather.GetProperty("precipitationProbability").GetDouble(),
						CloudCover = (int)weather.GetProperty("cloudCover").GetDouble()
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