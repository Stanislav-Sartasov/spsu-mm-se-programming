using WeatherApp.Container;
using WeatherApp.Sites.OpenWeatherMap;
using WeatherApp.Sites.TomorrowIo;

namespace WeatherApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This program implements the output of weather data in St.Petersburg");

            var sites = IoCContainer.GetServices();

            Console.WriteLine("Use 'update' to get weather data, 'exit' to terminate the program");
            Console.WriteLine("'disable' to disable one of services, 'add' to add service");

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
                else if (command == "disable")
                {
                    Console.WriteLine("Use 1 to disable OpenWeatherMap, 2 to disable TomorrowIo, or anything else to cancel");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            IoCContainer.Disable(typeof(OpenWeatherMapWeatherService));
                            break;
                        case "2":
                            IoCContainer.Disable(typeof(TomorrowIoWeatherService));
                            break;
                    }
                    sites = IoCContainer.GetServices();
                }
                else if (command == "add")
                {
                    Console.WriteLine("Use 1 to add OpenWeatherMap, 2 to add TomorrowIo, or anything else to cancel");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            IoCContainer.Connect(typeof(OpenWeatherMapWeatherService));
                            break;
                        case "2":
                            IoCContainer.Connect(typeof(TomorrowIoWeatherService));
                            break;
                    }
                    sites = IoCContainer.GetServices();
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
                Console.WriteLine("Use 'update' to update weather data, 'exit' to terminate the program");
                Console.WriteLine("'disable' to disable one of services, 'add' to add service");
            }
        }
    }
}