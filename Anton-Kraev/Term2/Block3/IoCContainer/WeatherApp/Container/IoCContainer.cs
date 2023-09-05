using WeatherApp.ISite;
using WeatherApp.Sites.OpenWeatherMap;
using WeatherApp.Sites.TomorrowIo;

namespace WeatherApp.Container
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