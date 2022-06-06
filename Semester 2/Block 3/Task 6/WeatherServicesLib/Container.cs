using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherServicesLib
{
	public class Container
	{
		private List<WeatherServices> availableServicesNames;
		private IServiceProvider serviceProvider;

		public Container(List<WeatherServices> servicesToCreate)
		{
			availableServicesNames = servicesToCreate;

			serviceProvider = Host.CreateDefaultBuilder()
				.ConfigureServices(services => services
				.AddSingleton<IWeatherService, Openweather>(s => availableServicesNames.Contains(WeatherServices.Openweather) ? new Openweather() : null)
				.AddSingleton<IWeatherService, Stormglass>(s => availableServicesNames.Contains(WeatherServices.Stormglass) ? new Stormglass() : null))
				.Build().Services;
		}
		
		public List<IWeatherService> GetAvailableServicesList()
		{
			List<IWeatherService> allServices = serviceProvider.GetServices<IWeatherService>().ToList();
			List<IWeatherService> nonNullServices = new List<IWeatherService>();
			foreach (var service in allServices)
			{
				if (service != null)
					nonNullServices.Add(service);
			}

			return nonNullServices;
		}
	}
}