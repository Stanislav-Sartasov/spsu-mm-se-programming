using WeatherRequesterResourceLibrary;
using WebWeatherRequester;

namespace ConsoleWeather
{
	public static class ConsoleMessages
	{
		private static string[] WindDirections = new string[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };

		public static void SendGreetings()
		{
			Console.WriteLine("This program was designed to output actual weather data");
			Console.WriteLine("from open web resources.\n");
		}

		public static void SendAPIKeyRequest()
		{
			Console.WriteLine("To fully operate, it requires API keys for the following services:");
		}

		public static string GetAPIKey(string serviceName)
		{
			Console.Write($"{serviceName}: ");
			var key = Console.ReadLine();
			return key == null ? string.Empty : key.Trim();
		}

		private static string GetWindDirection(double angle)
		{
			angle += 22.5;
			int direction = ((int)angle / 45) % 8;
			return WindDirections[direction];
		}

		public static void SendWeatherData(WeatherDataContainer data)
		{
			Console.WriteLine($"Source: {data.SourceName}");
			Console.WriteLine($"Temperature: {data.TempC:F2} C, {data.TempC * 9 / 5 + 32:F2} F");
			Console.WriteLine($"Humidity: {data.Humidity}%");
			Console.WriteLine($"Wind Speed: {data.WindSpeed:F2} m/s");
			Console.WriteLine($"Wind Direction: {GetWindDirection(data.WindDirection)}");
			Console.WriteLine($"Precipitation: {data.Precipitation}");
			Console.WriteLine($"Cloud Cover: {data.CloudCover}%");

			if (data.PrecipitationProbability != null)
				Console.WriteLine($"Precipitation Probability: {data.PrecipitationProbability:F0}%");
			if (data.PrecipitationVolumetric != null)
				Console.WriteLine($"Precipitation Height: {data.PrecipitationVolumetric:F2} mm");
		}

		public static void SendLogInfo(FetchWeatherLog log)
		{
			Console.WriteLine($"{log.Status}");
			Console.WriteLine($"{log.Message}");
		}

		public static void SendEachSourceData(IEnumerable<IWeatherRequester> sources)
		{
			foreach (var source in sources)
			{
				var data = source.FetchWeatherData();
				var log = source.GetLastLog();
				if (data != null)
					SendWeatherData(data);
				else if (log != null)
					SendLogInfo(log);
				else
					Console.WriteLine($"no data available for {source.GetType}; and no error invoked");
				Console.WriteLine();
			}
			if (sources.Count() == 0)
			{
				Console.WriteLine("No weather services were presented.");
				Console.WriteLine();
			}
		}

		public static char GetNextCommand(string message = "")
		{
			if (message.Length > 0)
				Console.WriteLine(message);
			while (true)
			{
				Console.WriteLine("Type <U> to update available data, <S> to list all available sources,");
				Console.Write("<A> to include another source, <R> to remove one,  <C> to quit: ");
				string userInput = Console.ReadLine().TrimEnd();
				Console.WriteLine();
				if (userInput.Length == 1)
					return userInput[0];
				Console.Write("unknown command. ");
			}
		}

		public static string GetSourceName()
		{
			Console.Write("Write the source name you wish to add/remove: ");
			return Console.ReadLine().Trim();
		}

		public static void SendAvailableSources(IEnumerable<string> sources)
		{
			Console.WriteLine("The available sources are the following: ");
			if (sources.Count() == 0)
				Console.WriteLine("    no sources available");
			else
				foreach (string source in sources)
					Console.WriteLine($"    {source}");
			Console.WriteLine();
		}
	}
}
