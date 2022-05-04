using WeatherWebAPI;
using WeatherLibrary;

namespace WeatherConsoleApp
{
    public static class Program
    {
        static void Main()
        {
            AParser tomorrowParser = new TomorrowParser();
            AParser stormGlassParser = new StormGlassParser();
            AWeather weatherFromTomorrow;
            AWeather weatherFromStormGlass;
            System.ConsoleKeyInfo keyPressed;
            bool willExit = false;

            while (!willExit)
            {
                PrintCityAndCurTime("Saint-Petersburg");

                weatherFromTomorrow = tomorrowParser.GetWeather();
                weatherFromStormGlass = stormGlassParser.GetWeather();

                PrintWeatherStat(weatherFromTomorrow);
                PrintWeatherStat(weatherFromStormGlass);

                PrintKeyboardInfo();

                keyPressed = Console.ReadKey();

                if (keyPressed.Key != ConsoleKey.Enter)
                {
                    willExit = true;
                }
            }
        }

        private static void PrintWeatherStat(AWeather weather)
        {
            Console.WriteLine("----------------------------");

            Console.WriteLine($"Source: {weather.SourceName}:\n");
            Console.WriteLine($"Temperature: {weather.TemperatureCelsius}, {weather.TemperatureFahrenheit}");
            Console.WriteLine($"Cloud cover: {weather.CloudCoverage}");
            Console.WriteLine($"Humidity: {weather.Humidity}");
            Console.WriteLine($"Precipitation: {weather.Precipitation}");
            Console.WriteLine($"WindSpeed: {weather.WindSpeed}");
            Console.WriteLine($"Wind direction from north: {weather.WindDirection}");

            Console.WriteLine("----------------------------");
        }

        private static void PrintKeyboardInfo()
        {
            Console.WriteLine("If you want to update the weather, please press the Enter button");
            Console.WriteLine("If you want the app to finish it's work, please press any another bottom\n\n");
        }

        private static void PrintCityAndCurTime(string city)
        {
            Console.WriteLine($"{city}, {DateTime.Now}");
        }
    }
}