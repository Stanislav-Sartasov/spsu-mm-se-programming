using Forecast;

namespace ConsoleOutputManagement
{
    public static class ConsoleOutput
    {

        public static void WriteWeather(SiteWeatherForecast weather)
        {
            var source = weather.Source;
            var time = DateTime.Now;
            var line = string.Concat(Enumerable.Repeat("-", 20));
            double counter = 0;

            if (weather.Forecast == null)
            {
                Console.WriteLine(line + "\n");
                Console.WriteLine($"Failed to get data from {source}.\nError message: {weather.ErrorMessage}\n");
                Console.WriteLine(line + "\n");
                return;
            }

            Console.WriteLine("\n" + line + $"\nSource: {source}\n");

            foreach (var day in weather.Forecast)
            {
                Console.WriteLine($"Data: {time.AddDays(counter).ToString("g")}\nCelsius temperature: {ProcessData(day.CelsiusTemperature, "°C")}\n" +
                                  $"Fahrenheit temperature: {ProcessData(day.FahrenheitTemperature, "°F")}\nHumidity: {ProcessData(day.Humidity, "%")}\n" +
                                  $"Cloud cover: {ProcessData(day.CloudCover, "%")}\nWind speed: {ProcessData(day.WindSpeed, "m/s")} \n" +
                                  $"Wind direction: {ProcessData(day.WindDirection, "°")}\n");
                counter++;
            }

            Console.WriteLine(line + "\n");
        }

        private static string ProcessData(string source, string additionalSymbol)
        {
            return source == "No data" ? "No data" : source + additionalSymbol;
        }
    }
}
