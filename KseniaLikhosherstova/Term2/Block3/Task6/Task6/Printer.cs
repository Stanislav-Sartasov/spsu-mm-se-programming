using Model;

namespace Task6
{
    public static class Printer
    {
        internal static Choice Greetings()
        {
            Console.WriteLine("The program shows the current weather in Saint Petersburg, receiving data from OpenWeatherMap and TomorrowIo.");
            Console.WriteLine("Select an action:\n1) Update\n2) Exit");

            int.TryParse(Console.ReadLine(), out var action);
            if (action != 1 && action != 2)
            {
                NonValid();
                Greetings();
            }

            if (action == 2)
                return Choice.Exit;

            Console.WriteLine("Select a service:\n1) OpenWeatherMap\n2) TommorowIo\n3) Both");

            Enum.TryParse<Choice>(Console.ReadLine(), out var choose);

            if (choose != Choice.OpenWeatherMap && choose != Choice.TommorowIo && choose != Choice.Both)
            {
                NonValid();
                Greetings();
            }

            return choose;
        }

        private static void NonValid()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine("Incorrect data was entered. Please try again.");
            Console.ResetColor();
        }

        public static void PrintWeather(WeatherInfo weather)
        {
            Console.WriteLine("Temperature: " + weather.TempC + " °C (" + weather.TempF + " °F)");
            Console.WriteLine("Wind: " + weather.WindSpeed + " m/s (" + weather.WindDirection + ")");
            Console.WriteLine("Cloud coverage: " + weather.CloudsPercent + "%");
            Console.WriteLine("Precipitation: " + weather.Precipitation);
            Console.WriteLine("Humidity: " + weather.Humidity + "%" + Environment.NewLine);
        }
    }
}
