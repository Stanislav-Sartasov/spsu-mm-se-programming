using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Task_6
{
	public class IoCContainer
	{
		public static List<ISite> GetRequest(string[] sources)
		{
			var services = Host.CreateDefaultBuilder()
				.ConfigureServices(services => 
					services.AddSingleton<IRequest, Request>()
					.AddTransient<TomorrowIo>()
					.AddTransient<OpenWeather>()
				)
				.Build().Services;

			var result = new List<ISite>();
			for (int i = 0; i < sources.Length; i++)
			{
				if (sources[i].ToLower() == "tomorrowio")
					result.Add(services.GetService<TomorrowIo>());
				if (sources[i].ToLower() == "openweather")
					result.Add(services.GetService<OpenWeather>());
			}
			return result;
		}
	}
}