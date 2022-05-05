using TomorrowIOAPI;
using StormglassIOAPI;
using WeatherRequesterResourceLibrary;
using WebWeatherRequester;
using IoCBuilder;
using System.Reflection;

namespace ConsoleWeather
{
	public class Program
	{
		private static string[] ServiceNames =
		{
			"TomorrowIO",
			"StormglassIO"
		};
		private static Type[] ServiceHandlers =
		{
			typeof(TomorrowIOHandler),
			typeof(StormglassIOHandler)
		};

		public static int Main()
		{
			ConsoleMessages.SendGreetings();
			ConsoleMessages.SendAPIKeyRequest();

			WebServicesBuilder serviceBuilder = new WebServicesBuilder();

			for (int i = 0; i < ServiceNames.Length; i++)
			{
				serviceBuilder.AddProvidedWebAPI(
					(AWebServiceAPI)Activator.CreateInstance(ServiceHandlers[i], ConsoleMessages.GetAPIKey(ServiceNames[i])),
					ServiceNames[i]);
			}

			IEnumerable<IWeatherRequester> requesters = serviceBuilder.GetWeatherRequesters();

			string prevResult = "";
			bool updated = false;
			while (true)
			{
				char command = ConsoleMessages.GetNextCommand(prevResult);
				switch (command)
				{
					case 'U':
						if (updated)
							requesters = serviceBuilder.GetWeatherRequesters();
						updated = false;
						ConsoleMessages.SendEachSourceData(requesters);
						prevResult = "";
						break;
					case 'S':
						ConsoleMessages.SendAvailableSources(ServiceNames);
						prevResult = "";
						break;
					case 'A':
						if (serviceBuilder.IncludeWebService(ConsoleMessages.GetSourceName()))
						{
							prevResult = "Service is successfully loaded!";
							updated = true;
						}
						else
							prevResult = "Service was not loaded; possibly, misspelled or already loaded";
						break;
					case 'R':
						if (serviceBuilder.ExcludeWebService(ConsoleMessages.GetSourceName()))
						{
							prevResult = "Service is successfully unloaded!";
							updated = true;
						}
						else
							prevResult = "Service was not unloaded; possibly, misspelled or already unloaded";
						break;
					case 'C':
						return 0;
					default:
						prevResult = "unknown command";
						break;
				}
			}

			return 0;
		}
	}
}