using Microsoft.Extensions.DependencyInjection;
using SiteInterface;
using Sites;
using Microsoft.Extensions.Hosting;

namespace Container
{
    public static class IoCContainer
    {
        public static List<SitesName> ConnectedSites { get; private set; } = new List<SitesName>
        {
            SitesName.OpenWeather, SitesName.StormGlass, SitesName.TomorrowIO
        };

        public static List<ISite> GetSites(WeatherParameter parameter)
        {
            var services = Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                    services.AddSingleton<ISite, TomorrowIO>(svc => ConnectedSites.Contains(SitesName.TomorrowIO) ? new TomorrowIO(parameter) : null)
                        .AddSingleton<ISite, OpenWeather>(svc => ConnectedSites.Contains(SitesName.OpenWeather) ? new OpenWeather(parameter) : null)
                        .AddSingleton<ISite, StormGlass>(svc => ConnectedSites.Contains(SitesName.StormGlass) ? new StormGlass(parameter) : null)
                )
                .Build().Services;

            return services.GetServices<ISite>().Where(x => x != null).ToList();
        }

        public static bool RemoveSiteFromContainer(SitesName name)
        {
            if (ConnectedSites.Contains(name))
            {
                ConnectedSites.Remove(name);
                return true;
            }

            return false;
        }

        public static bool AddSiteToContainer(SitesName name)
        {
            if (!ConnectedSites.Contains(name))
            {
                ConnectedSites.Add(name);
                return true;
            }

            return false;
        }
    }
}