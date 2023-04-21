using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherAPI;

namespace WeatherloC
{
	public class Container
	{
		public IServiceProvider Services;
		
		public Container(bool tomorrowIo, bool stormGlassIo)
		{
			var builder = Host.CreateDefaultBuilder();

			if (tomorrowIo)
				builder.ConfigureServices(services => services.AddSingleton<AWeatherAPI, TomorrowAPI>());
			if (stormGlassIo)
				builder.ConfigureServices(services => services.AddSingleton<AWeatherAPI, StormGlassAPI>());

			Services = builder.Build().Services;
		}

		public IEnumerable<AWeatherAPI> GetWeatherAPIs()
		{
			return Services.GetServices<AWeatherAPI>();
		}
	}
}