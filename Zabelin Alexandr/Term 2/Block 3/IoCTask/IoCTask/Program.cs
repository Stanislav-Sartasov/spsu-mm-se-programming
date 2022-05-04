using WeatherWebAPI;
using WeatherLibrary;
using IoC;

namespace IoCTask
{
    public static class Program
    {
        static void Main()
        {
            Console.WriteLine("If you want to get the weather from TomorrowIO, please write TomorrowIO and press Enter");
            Console.WriteLine("If you want to get the weather from StormGlassIO, please write StormGlass and press Enter");
            Console.WriteLine("If you want to get the weather from both source, please press Enter");

            Console.Write("Your choose: ");
            string? choose = Console.ReadLine();

            List<AParser> parsers = Container.GetParser(choose);

            Console.Clear();

            WorkInLoop(parsers);
        }

        private static void WorkInLoop(List<AParser> parsers)
        {
            System.ConsoleKeyInfo keyPressed;
            bool willExit = false;

            while (!willExit)
            {
                PrintCityAndCurTime("Saint-Petersburg");

                foreach (AParser parser in parsers)
                {
                    PrintWeatherStat(parser.GetWeather());
                }

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