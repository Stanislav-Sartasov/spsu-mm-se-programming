using Microsoft.Extensions.DependencyInjection;
using Model;

namespace Task6
{
    public static class IoCContainer
    {
        public static IEnumerable<IApi> Container()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddTransient<IApi>(x => new OpenWeatherMapApi(
                Config.OpenWeatherMapApiConfig.lat,
                Config.OpenWeatherMapApiConfig.lon,
                Config.OpenWeatherMapApiConfig.api));

            services.AddTransient<IApi>(x => new TomorrowIoApi(
                Config.TommorowIoApiConfig.lat,
                Config.TommorowIoApiConfig.lon,
                Config.TommorowIoApiConfig.api));

            var provider = services.BuildServiceProvider();

            var apis = provider.GetServices<IApi>();
            return apis;
        }
    }
}
