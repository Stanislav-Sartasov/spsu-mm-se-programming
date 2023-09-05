using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WeatherWebAPI;
using WebRequests;

namespace IoC
{
	public static class Container
	{
		public static List<AParser> GetParser(string? source)
		{
			var services = Host.CreateDefaultBuilder()
				.ConfigureServices(builder => builder
				.AddSingleton<IRequestMaker, RequestMaker>()
				.AddTransient<TomorrowParser>()
				.AddTransient<StormGlassParser>())
				.Build().Services;
			List<AParser> parsers = new List<AParser>();

			if (source == "TomorrowIO")
            {
				parsers.Add(services.GetService<TomorrowParser>());
            }
			else if (source == "StormGlass")
            {
				parsers.Add(services.GetService<StormGlassParser>());
			}
			else
            {
				parsers.Add(services.GetService<TomorrowParser>());
				parsers.Add(services.GetService<StormGlassParser>());
			}

			return parsers;
		}
	}
}