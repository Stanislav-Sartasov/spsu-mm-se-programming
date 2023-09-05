using WeatherConsoleApp.WeatherInfo;

namespace WeatherConsoleApp.ISite
{
    public static class ConsoleWriter
    {
        public static void ShowWeather(Weather weather)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("Temperature: " + weather.TempInCelcius + "°C | " + weather.TempInFahrenheit + "°F");
            Console.WriteLine("Humidity: " + weather.Humidity + "%");
            Console.WriteLine("Cloudiness: " + weather.Cloudiness + "%");
            Console.WriteLine("Precipitation: " + weather.Precipitation);
            Console.WriteLine("Wind speed: " + weather.WindSpeed + "m/s");
            Console.WriteLine("Wind direction: " + weather.WindDir + "\n");

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}