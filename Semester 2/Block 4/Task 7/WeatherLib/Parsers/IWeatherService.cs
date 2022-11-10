using WeatherLib.ResponceReceiver;
using WeatherLib.Weather;

namespace WeatherLib.Parsers
{
	public interface IWeatherService
	{
		WeatherServices Name { get; }
		string URL { get; }
		public WeatherForecast GetWeatherForecast(IResponceReceiver responce);
	}
}
