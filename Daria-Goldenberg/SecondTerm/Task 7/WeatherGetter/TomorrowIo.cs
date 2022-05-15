using Newtonsoft.Json.Linq;

namespace WeatherGetter
{
	public class TomorrowIo : Site
	{
		public TomorrowIo(IRequest request) : base(request)
		{
			Name = "TomorrowIo";
			url = "https://api.tomorrow.io/v4/timelines?&timesteps=current&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&location=59.931597,%2030.360431&apikey=";
			apiKey = "lSKBwB0KBgf1PfSSoQcJg2FwfUfoSdfT";
		}

		public override Weather GetData()
		{
			request.Run(url + apiKey);

			if (!request.Connected)
				throw new Exception("\"Tomorrow.io\" is unreachable.");
			else
			{
				var json = JObject.Parse(request.Response);

				var values = json["data"]["timelines"][0]["intervals"][0]["values"];
				double temperatureCelsius = Convert.ToDouble(values["temperature"]);
				double temperatureFahrenheit = Math.Round((double)(temperatureCelsius * 1.8 + 32), 2);
				double windSpeed = Convert.ToDouble(values["windSpeed"]);
				string windDirection = CheckDirection(Convert.ToInt32(values["windDirection"]));
				double cloudCover = Convert.ToDouble(values["cloudCover"]);
				string precipitation = CheckPrecipitationType(Convert.ToInt32(values["precipitationType"]));
				double humidity = Convert.ToDouble(values["humidity"]);

				return new Weather(temperatureCelsius, temperatureFahrenheit, cloudCover, humidity, windSpeed, windDirection, precipitation);
			}
		}

		private string CheckPrecipitationType(int type)
		{
			if (type == 0)
				return "Clear";
			else if (type == 1)
				return "Rain";
			else if (type == 2)
				return "Snow";
			else if (type == 3)
				return "Freezing Rain";
			else if (type == 4)
				return "Ice Pellets";
			return "Undefined";
		}
	}
}
