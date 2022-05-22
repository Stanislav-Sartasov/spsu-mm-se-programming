using WeatherForecastModel;

namespace Weather
{
	public static class ConsoleWriter
	{
		public static void WriteException(string message) => Console.WriteLine($"Exception occured: {message}.\n");

		public static void WriteGreeting()
		{
			Console.WriteLine("________-------- WEATHER FORECAST --------________");
			Console.WriteLine();
			Console.WriteLine("DESCRIPTION:");
			Console.WriteLine();
			Console.WriteLine("The program gets the weather forecast from the following webcites:");
			Console.WriteLine(" * https://stormglass.io/");
			Console.WriteLine(" * https://www.tomorrow.io/");
			Console.WriteLine("and then writes result in console.");
			Console.WriteLine();
			Console.WriteLine();
		}

		public static void WriteErrorTomorrow() => Console.WriteLine("Something went wrong with Tomorrow.io forecast.\n");

		public static void WriteErrorStormGlass() => Console.WriteLine("Something went wrong with StormGlass.io forecast.\n");

		public static void WriteWeatherForecast(WeatherModel model, string webcite)
		{
			Console.WriteLine($"________-------- {webcite} --------________");
			Console.WriteLine();
			Console.WriteLine("The weather in Saint Petersburg:");
			Console.WriteLine($" * Temperature in Celsius - {model.Temperature} degrees.");
			double temperature = Convert.ToDouble(model.Temperature.Replace(".", ","));
			temperature = Math.Round(temperature * 9 / 5 + 32, 2);
			Console.WriteLine($" * Temperature in Fahrenheit - {temperature} degrees.");
			Console.WriteLine($" * Humidity - {model.Humidity}%.");
			Console.WriteLine($" * Probability of precipitation - {model.PrecipitationProbability}%.");
			Console.WriteLine($" * Сloudiness - {model.CloudCover}%.");
			Console.WriteLine($" * Wind speed - {model.WindSpeed} m/s.");
			Console.WriteLine($" * Wind Direction(comes from) - {model.WindDirection} degrees clockwise from due north.");
			Console.WriteLine();
			Console.WriteLine();
		}

		public static void WriteAboutKeybord() => Console.WriteLine("Press 'Enter' to update forecast.\nPress 'Esc' to finish the program.\n");
	}
}