using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;

namespace WeatherLib.Parsers
{
	public class Stormglass : IWeatherService
	{
		public WeatherServices Name { get; private set; }
		public string URL { get; private set; }
		
		private const string latitude = "59.9386";
		private const string longitude = "30.3141";
		private const string parameters = "airTemperature,cloudCover,humidity,precipitation,windWaveDirection,windSpeed";
		private const string key = "660840ca-c942-11ec-9863-0242ac130002-66084142-c942-11ec-9863-0242ac130002";

		public Stormglass()
		{
			Name = WeatherServices.Stormglass;

			DateTime now = DateTime.UtcNow;
			string start = now.ToString("yyyy-MM-ddTHH:mm:ss");
			URL = $"https://api.stormglass.io/v2/weather/point?lat={latitude}&lng={longitude}&params={parameters}&start={start}&end={start}&key={key}";
		}

		public WeatherForecast GetWeatherForecast(IResponceReceiver responceTask)
		{
			IResponceReceiver responce = responceTask;

			JObject json;
			if (responce.IsSucceed && responce.Responce != null)
			{
				json = JObject.Parse(responce.Responce);
			}
			else
			{
				return new WeatherForecast(false);
			}

			WeatherForecast forecast = new WeatherForecast(
				json["hours"][0]["airTemperature"]["sg"].ToString(),
				json["hours"][0]["cloudCover"]["sg"].ToString(),
				json["hours"][0]["humidity"]["sg"].ToString(),
				json["hours"][0]["precipitation"]["sg"].ToString(),
				json["hours"][0]["windSpeed"]["sg"].ToString(),
				json["hours"][0]["windWaveDirection"]["sg"].ToString(),
				Name);

			return forecast;
		}
	}
}