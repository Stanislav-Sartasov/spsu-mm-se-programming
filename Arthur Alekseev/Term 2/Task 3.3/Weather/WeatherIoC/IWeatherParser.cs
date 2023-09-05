
namespace WeatherIoC
{
	public interface IWeatherParser
	{
		public string Name { get; }
		public WeatherData CollectData();
	}
}
