using WeatherLib.ISite;
using WeatherLib.Sites.OpenWeatherMap;
using WeatherLib.Sites.TomorrowIo;

namespace WeatherLib.Container
{
    public static class IoCContainer
    {
        private static SimpleInjector.Container container;

        private static List<Type> connectedSites = new()
        {
            typeof(OpenWeatherMapWeatherService),
            typeof(TomorrowIoWeatherService)
        };

        public static List<IWeatherService> GetServices()
        {
            container = new SimpleInjector.Container();

            container.Collection.Register<IWeatherService>(connectedSites);
            
            container.Verify();

            return container.GetAllInstances<IWeatherService>().ToList();
        }

        public static void Connect(Type serviceType)
        {
            if (serviceType.GetInterfaces().Contains(typeof(IWeatherService)) && !connectedSites.Contains(serviceType))
            {
                connectedSites.Add(serviceType);
            }
        }

        public static void Disable(Type serviceType)
        {
            if (connectedSites.Contains(serviceType))
            {
                connectedSites.Remove(serviceType);
            }
        }
    }
}