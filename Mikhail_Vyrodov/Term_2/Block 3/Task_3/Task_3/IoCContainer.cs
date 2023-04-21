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
        private List<IWeatherDisplayer> displayers;

        public List<IWeatherDisplayer> GetDisplayers()
        {
            FillDisplayer(true);
            FillDisplayer(false);
            return displayers;
        }

        public IoCContainer()
        {
            displayers = new List<IWeatherDisplayer>();
        }

        private void FillDisplayer(bool creatingTomorrowio)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices(services => services.AddSingleton<TomorrowioWeatherDisplayer>()
                .AddSingleton<IParametersFiller<TomorrowDataHolder>, TomorrowioParametersFiller>()
                .AddSingleton<IJsonMapper<TomorrowDataHolder>, TomorrowioMapper>()
                .AddSingleton<IParametersFiller<StormglassDataHolder>, StormglassioParametersFiller>()
                .AddSingleton<IJsonMapper<StormglassDataHolder>, StormglassioMapper>()
                .AddSingleton<ConsoleWriter>()
                .AddSingleton<IResponseReader, ResponseReader>()
                .AddSingleton<IWebServerHelper, TomorrowioWebHelper>()
                .AddSingleton<IRequestURLFiller, TomorrowioRequestURLFiller>()
                .AddSingleton<StormglassioWeatherDisplayer>()
                );
            if (creatingTomorrowio)
            {
                builder.ConfigureServices(services => services.AddSingleton<IWebServerHelper, TomorrowioWebHelper>()
                .AddSingleton<IRequestURLFiller, TomorrowioRequestURLFiller>());
                IServiceProvider services = builder.Build().Services;
                displayers.Add(services.GetService<TomorrowioWeatherDisplayer>());
            }
            else
            {
                builder.ConfigureServices(services => services.AddSingleton<IWebServerHelper, StormglassioWebHelper>()
                .AddSingleton<IRequestURLFiller, StormglassioRequestURLFiller>());
                IServiceProvider services = builder.Build().Services;
                displayers.Add(services.GetService<StormglassioWeatherDisplayer>());
            }
        }
    }
}
