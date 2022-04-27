using Microsoft.Extensions.DependencyInjection;
using SiteInterface;
using Sites;
using Microsoft.Extensions.Hosting;

namespace Container
{
    public static class IoCContainer
    {
        public static List<ISite> GetSites(WeatherParameter parameter)
        {
            var services = Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                    services.AddSingleton<ISite, TomorrowIO>(svc => new TomorrowIO(parameter))
                        .AddSingleton<ISite, OpenWeather>(svc => new OpenWeather(parameter))
                        .AddSingleton<ISite, StormGlass>(svc => new StormGlass(parameter))
                )
                .Build().Services;

            return services.GetServices<ISite>().ToList();
        }
    }
}