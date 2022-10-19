using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherUI_WPF.Service
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
                Config.TomorrowIoApiConfig.lat,
                Config.TomorrowIoApiConfig.lon,
                Config.TomorrowIoApiConfig.api));

            var provider = services.BuildServiceProvider();

            var apis = provider.GetServices<IApi>();
            return apis;
        }
    }
}