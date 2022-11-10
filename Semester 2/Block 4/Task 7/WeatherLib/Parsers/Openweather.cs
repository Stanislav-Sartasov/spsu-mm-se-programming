using Newtonsoft.Json.Linq;
using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;

namespace WeatherLib.Parsers
{
	public class Openweather : IWeatherService
	{
		public WeatherServices Name { get; private set; }
		public string URL { get; private set; }

		private const string latitude = "59.9386";
		private const string longitude = "30.3141";
		private const string units = "metric";
		private const string key = "612ff6587b1aa1997d794833bb3c37ee";

		public Openweather()
		{
			Name = WeatherServices.Openweather;

			URL = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&units={units}&appid={key}";
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
				json["main"]["temp"].ToString(),
				json["clouds"]["all"].ToString(),
				json["main"]["humidity"].ToString(),
				null,
				json["wind"]["speed"].ToString(),
				json["wind"]["deg"].ToString(),
				Name);

			return forecast;
		}
	}
}