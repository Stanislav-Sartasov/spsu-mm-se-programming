using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIoC
{
	public class IoCContainer
	{

		public IoCContainer()
		{

		}

		public List<IWeatherParser> CreateParsers(bool generateOpenWeatherMap, bool generateTomorrowIo)
		{
			var builder = Host.CreateDefaultBuilder();
			builder.ConfigureServices(builder => builder.AddTransient<OpenWeatherMapParser>()
				.AddSingleton<IWebParser, WebParser>()
				.AddSingleton<WebParser>()
				.AddTransient<TomorrowIoParser>()
				.AddSingleton<IWebParser, WebParser>()
				.AddSingleton<WebParser>());
			var services = builder.Build().Services;

			var result = new List<IWeatherParser>();
			if (generateOpenWeatherMap)
				result.Add(services.GetService<OpenWeatherMapParser>());
			if(generateTomorrowIo)
				result.Add(services.GetService<TomorrowIoParser>());
			return result;
		}

	}
}
