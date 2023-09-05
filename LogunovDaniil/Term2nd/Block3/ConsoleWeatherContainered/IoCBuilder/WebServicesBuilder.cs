using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebWeatherRequester;
using WeatherRequesterResourceLibrary;

namespace IoCBuilder
{
	public class WebServicesBuilder
	{
		private List<AWebServiceAPI> webAPIServices = new List<AWebServiceAPI>();
		private List<string> webAPIServicesNames = new List<string>();
		private List<bool> webAPIServicesToUse = new List<bool>();

		public void AddProvidedWebAPI(AWebServiceAPI service, string serviceName)
		{
			webAPIServices.Add(service);
			webAPIServicesNames.Add(serviceName);
			webAPIServicesToUse.Add(false);
		}

		public bool IncludeWebService(string serviceName)
		{
			if (!webAPIServicesNames.Contains(serviceName))
				return false;
			int index = webAPIServicesNames.IndexOf(serviceName);
			if (webAPIServicesToUse[index])
				return false;
			return webAPIServicesToUse[index] = true;
		}

		public bool ExcludeWebService(string serviceName)
		{
			if (!webAPIServicesNames.Contains(serviceName))
				return false;
			int index = webAPIServicesNames.IndexOf(serviceName);
			if (!webAPIServicesToUse[index])
				return false;
			webAPIServicesToUse[index] = false;
			return true;
		}

		public IEnumerable<IWeatherRequester> GetWeatherRequesters()
		{
			var weatherRequesters = Host.CreateDefaultBuilder();
			
			for (int i = 0; i < webAPIServicesNames.Count; i++)
			{
				if (webAPIServicesToUse[i])
				{
					var current = webAPIServices[i];
					weatherRequesters.ConfigureServices(services => services
					.AddSingleton<IWeatherRequester, WebWeather>(webService => new WebWeather(current))
					);
				}
			}

			return weatherRequesters.Build().Services.GetServices<IWeatherRequester>();
		}
	}
}
