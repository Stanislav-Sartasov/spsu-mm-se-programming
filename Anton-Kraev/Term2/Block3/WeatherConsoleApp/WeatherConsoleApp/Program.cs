using WeatherConsoleApp.ISite;
using WeatherConsoleApp.Sites.OpenWeatherMap;
using WeatherConsoleApp.Sites.TomorrowIo;

namespace WeatherConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This program implements the output of weather data in St.Petersburg");

            List<IWeatherService> sites = new List<IWeatherService>
            {
                new TomorrowIoWeatherService(),
                new OpenWeatherMapWeatherService()
            };

            Console.WriteLine("Use 'update' to get weather data, 'exit' to terminate the program");

            string? command;
            while ((command = Console.ReadLine()) != "exit")
            {
                if (command == "update")
                {
                    Console.Clear();

                    Console.WriteLine("**The data is current at " + DateTime.Now.ToString("HH:mm") + "**\n");

                    foreach (var site in sites)
                    {
                        site.ShowWeather();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
                Console.WriteLine("Use 'update' to update weather data, 'exit' to terminate the program");
            }
        }
    }
}