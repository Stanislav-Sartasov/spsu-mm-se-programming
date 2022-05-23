using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JsonParsingLibrary;

namespace Task_3
{
    public class IoCContainer
    {
        public bool CreatingTomorrowio = false, CreatingStormglassio = false;

        public IWeatherDisplayer CreateDisplayer()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices(services => services.AddSingleton<IParametersFiller<TomorrowDataHolder>, TomorrowioParametersFiller>()
                .AddSingleton<IJsonMapper<TomorrowDataHolder>, TomorrowioMapper>()
                .AddSingleton<IParametersFiller<StormglassDataHolder>, StormglassioParametersFiller>()
                .AddSingleton<IJsonMapper<StormglassDataHolder>, StormglassioMapper>()
                .AddSingleton<ConsoleWriter>()
                .AddSingleton<IResponseReader, ResponseReader>()
                );
            if (CreatingTomorrowio)
            {
                builder.ConfigureServices(services =>
                services.AddSingleton<IWeatherDisplayer, TomorrowioWeatherDisplayer>()
                .AddSingleton<IWebServerHelper, TomorrowioWebHelper>()
                .AddSingleton<IRequestURLFiller, TomorrowioRequestURLFiller>()
                );
            }
            else if (CreatingStormglassio)
            {
                builder.ConfigureServices(services =>
                services.AddSingleton<IWeatherDisplayer, StormglassioWeatherDisplayer>()
                .AddSingleton<IWebServerHelper, StormglassioWebHelper>()
                .AddSingleton<IRequestURLFiller, StormglassioRequestURLFiller>()
                );
            }
            IServiceProvider services = builder.Build().Services;
            return services.GetService<IWeatherDisplayer>();
        }
    }
}
