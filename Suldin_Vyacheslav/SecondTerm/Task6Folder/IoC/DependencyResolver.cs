using System;
using Ninject;
using WebLibrary;
using Parsers;
using TomorrowIO;
using System.Reflection;
using System.Resources;
using System.Collections.Generic;
using StormGlass;
using OpenWeather;
using GisMeteo;
using System.Linq;

namespace IoC
{
    public class DependencyResolver

    {
        private static ResourceManager rm = new ResourceManager("IoC.BaseKeysSet", Assembly.GetExecutingAssembly());

        public IKernel Kernel = new StandardKernel();

        public Dictionary<Type, ServiceName> Setup = new Dictionary<Type, ServiceName>()
        {
            { typeof(GisMeteoParser), ServiceName.GisMeteoParser },
            { typeof(OpenWeatherParser), ServiceName.OpenWeatherParser },
            { typeof(TomorrowIOParser), ServiceName.TomorrowIOParser },
            { typeof(StormGlassParser), ServiceName.StormGlassParser }
        };


        private List<JSONParser> services;
        public List<JSONParser> GetParsers()
        {
            services = new List<JSONParser>();
            TryAdd<TomorrowIOParser>(services);
            TryAdd<OpenWeatherParser>(services);
            TryAdd<StormGlassParser>(services);
            TryAdd<GisMeteoParser>(services);

            return services;
        }

        private void TryAdd<T>(List<JSONParser> services)
            where T : JSONParser
        {
            if (Setup[typeof(T)] != ServiceName.Disabled)
            {
                services.Add(GetService<T>());
            }
        }

        private void BaseBind<T>()
            where T : JSONParser
        {
            Kernel.Rebind<IGetRequest>().To<GetRequest>();
            Kernel.Rebind<IJsonHolder>().To<JsonHolder>();
            Kernel.Rebind<JSONParser>().To<T>().WithConstructorArgument("json", Kernel.Get<JsonHolder>().Json);
        }
        public T GetService<T>()
            where T : JSONParser
        {
            Kernel.Rebind<IRequestable>().To<T>().WithConstructorArgument("key", rm.GetString(Setup[typeof(T)].ToString()));
            BaseBind<T>();
            return Kernel.Get<JSONParser>() as T;
        }
        public T GetService<T>(string key)
            where T : JSONParser
        {
            Kernel.Rebind<IRequestable>().To<T>().WithConstructorArgument("key", key);
            BaseBind<T>();
            return Kernel.Get<JSONParser>() as T;
        }
        public void Remove<T>()
            where T: JSONParser
        {
            Setup[typeof(T)] = ServiceName.Disabled;
        }
        public void Restore<T>()
            where T : JSONParser
        {
            foreach (ServiceName name in Enum.GetValues(typeof(ServiceName)))
            {
                if (typeof(T).Name == name.ToString())
                {
                    Setup[typeof(T)] = name;
                    return;
                }
            }
        }
    }
}
