namespace Task_6
{
	public static class Logger
	{
		public static void LogError(Exception ex)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(ex.Message);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void LogWeather(string name, Weather weather)
		{
			Console.WriteLine("---" + name + "---");
			Console.WriteLine("Temperature: " + CheckNull(weather.TemperatureCelsius) + "°C (" + CheckNull(weather.TemperatureFahrenheit) + "°F)");
			Console.WriteLine("Wind: " + CheckNull(weather.WindSpeed) + " m/s (" + CheckNull(weather.WindDirection) + ")");
			Console.WriteLine("Cloud coverage: " + CheckNull(weather.CloudCover) + "%");
			Console.WriteLine("Precipitation: " + CheckNull(weather.Precipitation));
			Console.WriteLine("Humidity: " + CheckNull(weather.Humidity) + "%" + Environment.NewLine);
		}

		private static object CheckNull(object? data)
		{
			if (data != null)
				return data;
			return "No Data";
		}
	}
}