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
		public bool GenerateOpenWeatherMap;
		public bool GenerateTomorrowIo;
		public IoCContainer()
		{
			GenerateOpenWeatherMap = true;
			GenerateTomorrowIo = true;
		}

		public List<IWeatherParser> CreateParsers()
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
			if (GenerateOpenWeatherMap)
				result.Add(services.GetService<OpenWeatherMapParser>());
			if(GenerateTomorrowIo)
				result.Add(services.GetService<TomorrowIoParser>());
			return result;
		}

	}
}
