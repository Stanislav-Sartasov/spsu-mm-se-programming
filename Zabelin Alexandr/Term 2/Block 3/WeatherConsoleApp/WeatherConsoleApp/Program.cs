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
            Weather weatherFromTomorrow;
            Weather weatherFromStormGlass;
            System.ConsoleKeyInfo keyPressed;
            bool willExit = false;

            while (!willExit)
            {
                Console.WriteLine($"Saint-Petersburg, {DateTime.Now}");

                weatherFromTomorrow = tomorrowParser.GetWeather();
                weatherFromStormGlass = stormGlassParser.GetWeather();

                weatherFromTomorrow.PrintAll();
                weatherFromStormGlass.PrintAll();

                Console.WriteLine("If you want to update the weather, please press the Enter button");
                Console.WriteLine("If you want the app to finish it's work, please press any another bottom\n\n");

                keyPressed = Console.ReadKey();

                if (keyPressed.Key != ConsoleKey.Enter)
                {
                    willExit = true;
                }
            }
        }
    }
}