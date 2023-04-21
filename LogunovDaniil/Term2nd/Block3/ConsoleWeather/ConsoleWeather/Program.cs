using TomorrowIOAPI;
using StormglassIOAPI;
using WeatherRequesterResourceLibrary;
using WebWeatherRequester;

namespace ConsoleWeather
{
	public class Program
	{
		public static int Main()
		{
			ConsoleMessages.SendGreetings();
			ConsoleMessages.SendAPIKeyRequest();

			List<IWeatherRequester> requesters = new List<IWeatherRequester>();

			requesters.Add(new WebWeather(new TomorrowIOHandler(ConsoleMessages.GetAPIKey("TomorrowIO").Trim())));
			requesters.Add(new WebWeather(new StormglassIOHandler(ConsoleMessages.GetAPIKey("StormglassIO").Trim())));

			Console.WriteLine();

			char command = 'U';
			while (command == 'U')
			{
				ConsoleMessages.SendEachSourceData(requesters);
				command = ConsoleMessages.GetNextCommand();
			}

			return 0;
		}
	}
}