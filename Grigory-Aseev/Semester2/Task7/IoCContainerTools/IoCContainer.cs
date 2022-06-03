using Autofac;
using SiteInterfaces;
using Sites;

namespace IoCContainerTools
{
    public static class IoCContainer
    {
        public static List<Type> Sites { get; private set; } = new List<Type>()
        {
            typeof(OpenWeatherMap),
            typeof(TomorrowIO),
            typeof(StormGlassIO)
        };

        public static IEnumerable<ISite> GetSites()
        {
            var builder = new ContainerBuilder();

            foreach (var type in Sites)
            {
                builder.RegisterType(type).As<ISite>();
            }

            var container = builder.Build();

            return container.Resolve<IEnumerable<ISite>>();
        }

        public static bool DisconnectSite(Type siteType) => Sites.Remove(siteType);

        public static bool ConnectSite(Type siteType)
        {
            bool condition = !Sites.Contains(siteType);
            if (condition)
            {
                Sites.Add(siteType);
            }
            return condition;
        }
    }
}