using AbstractWeatherForecast;
using Autofac;
using RequestApi;

namespace IoC
{
    public class ContainerConfig
    {
        private ContainerBuilder builder;

        public List<AWeatherForecast> WeatherForecasts { get; private set; }

        public ContainerConfig()
        {
            WeatherForecasts = new List<AWeatherForecast>();
        }

        private T GetService<T, M>(string key, int type) 
            where T : AWeatherForecast
            where M : AParser
        {
            builder = new ContainerBuilder();
            builder.RegisterType<ApiHelper>().AsSelf()
                .WithParameter("key", key)
                .WithParameter("type", type);

            builder.RegisterType<M>()
                .As<AParser>()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(ApiHelper) && pi.Name == "apiHelper",
                          (pi, ctx) => ctx.Resolve<ApiHelper>());
            builder.RegisterType<T>()
                .As<AWeatherForecast>()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(AParser) && pi.Name == "dataParser",
                          (pi, ctx) => ctx.Resolve<AParser>());
            return (T)builder.Build().Resolve<AWeatherForecast>();
        }

        public ContainerConfig AddService<T, M>(string key, int type) 
            where T : AWeatherForecast
            where M : AParser
        {
            if (!WeatherForecasts.Any(service => service is T))
                WeatherForecasts.Add(GetService<T, M>(key, type));

            return this;
        }

        public ContainerConfig RemoveService<T>() where T : AWeatherForecast
        {
            WeatherForecasts = WeatherForecasts.Where(x => x is not T).ToList();

            return this;
        }

        public bool IsServiceActive<T>()
            where T : AWeatherForecast
        {
            return WeatherForecasts.Any(x => x is T);
        }

        public T? GetWeatherForecast<T>()
            where T : AWeatherForecast
        {
            foreach (var ex in WeatherForecasts)
            {
                if (ex.GetType() == typeof(T))
                    return (T)ex;
            }
            return null;
        }
    }
}