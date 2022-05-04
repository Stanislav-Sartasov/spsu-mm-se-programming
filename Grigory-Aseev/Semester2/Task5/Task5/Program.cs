﻿using Sites;
using SiteInterfaces;

namespace Task5
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine($"https://api.stormglass.io/v2/weather/point?lat=59.93863&lng=30.31413&params=windDirection,windSpeed,airTemperature,cloudCover,humidity,precipitation&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&key=690cf524-ca21-11ec-a8d3-0242ac130002-690cf5ce-ca21-11ec-a8d3-0242ac130002");
            bool flag = true;
            List<ISite> sites = new List<ISite>();
            sites.Add(new OpenWeatherMap());
            sites.Add(new StormGlassIO());
            sites.Add(new TomorrowIO());
            Console.WriteLine("This program shows data (temperature (degrees and Fahrenheit), clouds, humidity, precipitation,\nwind direction and speed) of the current weather in the city of St. Petersburg from three different sources.");
            string sources = "===> openweathermap, tomorrow.io, stormglass.io <===";

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            PrintCentrally(sources);
            Console.ResetColor();

            Console.WriteLine();

            while (flag)
            {
                for (int i = 0; i < 3; i++)
                {
                    PrintCentrally($"Requesting data from the site: {sites[i].SiteAddress} ...");
                    sites[i].GetRequest();
                    PrintCentrally("The data is being processed...");
                    sites[i].Parse();
                }

                Console.WriteLine();
                WeatherPrinter.PrintForecast(sites);
                Console.WriteLine();

                PrintCentrally("Press Esc to exit or any other key to update the weather data");
                flag = Console.ReadKey(true).Key != ConsoleKey.Escape;
                Console.WriteLine("The old data is being cleaned up...");
                for (int i = 0; i < 3; i++)
                {
                    sites[i].Clear();
                }
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            PrintCentrally("I hope these sources helped you, thank you for using my application, see you soon ^_^");
            Console.ResetColor();
        }

        internal static void PrintCentrally(string text)
        {
            var width = Console.WindowWidth;
            var padding = width / 2 + text.Length / 2;
            Console.WriteLine("{0," + padding + "}", text);
        }
    }
}