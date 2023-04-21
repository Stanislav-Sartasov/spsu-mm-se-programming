using ResponceReceiverLib;

namespace WeatherServicesLib
{
	public interface IWeatherService
	{
		WeatherServices Name { get; }
		string URL { get; }
		public WeatherForecast GetWeatherForecast(IResponceReceiver responce);
	}
}
