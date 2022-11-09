using SimpleInjector;
using Sites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherContainer
{
	public class IoCContainer
	{
		private static Container container;
		private static List<Type> connectedSites = new();

		public static List<ISites> GetServices()
		{
			container = new Container();
			container.Collection.Register<ISites>(connectedSites);
			container.Verify();
			return container.GetAllInstances<ISites>().ToList();
		}

		public static void AddSites(Type serviceType)
		{
			if (serviceType.GetInterfaces().Contains(typeof(ISites)) && !connectedSites.Contains(serviceType))
				connectedSites.Add(serviceType);
		}

		public static void RemoveSites(Type serviceType)
		{
			if (connectedSites.Contains(serviceType))
				connectedSites.Remove(serviceType);
		}
	}
}
