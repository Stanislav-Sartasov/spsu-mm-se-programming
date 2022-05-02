using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Weather.Parsers
{
	public class Container
	{
		public static IParser CreateParser(string parameter)
		{
			var services = Host.CreateDefaultBuilder()
				.ConfigureServices(services => services
				.AddTransient<ParserOpenWeather>()
				.AddTransient<ParserTomorrowIo>()
				.AddSingleton<IHttpClient, MyHttpClient>())
				.Build().Services;

			if (parameter == "OpenWeather")
				return services.GetService<ParserOpenWeather>();
			else
				return services.GetService<ParserTomorrowIo>();
		}
	}
}
