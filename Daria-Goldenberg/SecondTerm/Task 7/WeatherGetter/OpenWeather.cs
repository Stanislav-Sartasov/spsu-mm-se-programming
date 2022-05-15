using Newtonsoft.Json.Linq;

namespace WeatherGetter
{
	public class OpenWeather : Site
	{
		public OpenWeather(IRequest request) : base(request)
		{
			Name = "OpenWeather";
			url = "http://api.openweathermap.org/data/2.5/weather?id=498817&appid=";
			apiKey = "7fc271e288a13c8fac3f7e6d4f20c625";
		}

		public override Weather GetData()
		{
			request.Run(url + apiKey);

			if (!request.Connected)
				throw new Exception("\"OpenWeather\" is unreachable.");
			else
			{
				var json = JObject.Parse(request.Response);

				var main = json["main"];
				double temperatureKelvin = Convert.ToDouble(main["temp"]);
				double temperatureCelsius = Math.Round(temperatureKelvin - 273.15, 2);
				double temperatureFahrenheit = Math.Round((double)(temperatureCelsius * 1.8 + 32), 2);
				double humidity = Convert.ToDouble(main["humidity"]);

				var wind = json["wind"];
				double windSpeed = Convert.ToDouble(wind["speed"]);
				string windDirection = CheckDirection(Convert.ToInt32(wind["deg"]));

				var clouds = json["clouds"];
				double cloudCover = Convert.ToDouble(clouds["all"]);

				string precipitation = Convert.ToString(json["weather"][0]["main"]);

				return new Weather(temperatureCelsius, temperatureFahrenheit, cloudCover, humidity, windSpeed, windDirection, precipitation);
			}
		}
	}
}