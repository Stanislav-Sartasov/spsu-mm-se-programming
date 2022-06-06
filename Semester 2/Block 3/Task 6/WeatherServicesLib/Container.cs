using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResponceReceiverLib;

namespace WeatherServicesLib
{
	public class Container
	{
		public static IWeatherService CreateWeatherService(WeatherServices serviceName)
		{
			var services = Host.CreateDefaultBuilder()
				.ConfigureServices(services => services
				.AddTransient<Openweather>()
				.AddTransient<Stormglass>()
				.AddSingleton<IResponceReceiver, ResponceReceiver>())
				.Build().Services;

			if (serviceName == WeatherServices.Openweather)
				return services.GetService<Openweather>();
			else
				return services.GetService<Stormglass>();
		}
	}
}