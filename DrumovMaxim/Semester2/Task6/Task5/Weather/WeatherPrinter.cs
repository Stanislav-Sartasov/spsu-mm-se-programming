using WeatherPattern;
using WeatherManagerAPI;

namespace Weather
{
    public static class WeatherPrinter
    {
        public static void ThrowError(string name)
        {
            Console.WriteLine($"An error occurred while fetching data from the site: {name}");
        }

        public static void PrintGreetings()
        {
            Console.WriteLine("This program is designed to receive weather data in the city\nof St. Petersburg and display them on the screen.\n");
            Console.WriteLine("We receive data from the following sites:");
            Console.WriteLine("tomorrow.io, stormglass.io\n");
        }

        public static void PrintWeather(WeatherPtrn? weather)
        {
            Console.WriteLine($"Cloud cover: {weather.CloudCover} %");
            Console.WriteLine($"Humidity: {weather.Humidity} %");
            Console.WriteLine($"Precipitation {weather.Precipitation} mm/hr");
            Console.WriteLine($"Temperature in Celsius: {weather.Temperature} °C");
            double TempFar = weather.Temperature * 1.8 + 32;
            Console.WriteLine($"Temperature in Fahrenheit: {Math.Round(TempFar, 1, MidpointRounding.ToEven)} °F");
            PrintHeatScale(weather.Temperature);
            Console.ResetColor();
            Console.WriteLine($"Wind direction: {weather.WindDirection} deg");
            Console.WriteLine($"Wind Speed: {weather.WindSpeed} m/s\n");
        }

        public static void PrintHeatScale(double temp)
        {
            Console.Write("Heat scale:");

            if (temp >= 25)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }else if (temp >= 10 && temp < 25)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }else if (temp > -10 && temp < 10)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }else if (temp <= -10)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.WriteLine(" ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■ ■");
        }

        public static void WeatherOutput(AManagerAPI weatherAPI)
        {
            WeatherPtrn? weather;

            try
            {
                Console.WriteLine($"Getting data from the site: {weatherAPI.Name}\n");
                weather = weatherAPI.GetWeather(weatherAPI.GetResponse(weatherAPI.WebAddress));

                if (weatherAPI.State)
                {
                    PrintWeather(weather);
                }
            }
            catch (Exception e)
            {
                
                Console.ForegroundColor = ConsoleColor.Red;
                ThrowError(weatherAPI.Name);
                Console.WriteLine($"Exception:\n{e.Message}\n");
                Console.ResetColor();
            }
        }

    }
}
