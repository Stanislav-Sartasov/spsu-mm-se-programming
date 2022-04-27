using WeatherConsoleApp.ApiRequestMaker;
using WeatherConsoleApp.WeatherInfo;
using OpenWeatherMap = WeatherConsoleApp.Sites.OpenWeatherMap;
using TomorrowIo = WeatherConsoleApp.Sites.TomorrowIo;

namespace WeatherConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("This program implements the output of weather data in St.Petersburg");

            AbstractApiRequestMaker openWeatherMap = new OpenWeatherMap.ApiRequestMaker();
            AbstractApiRequestMaker tomorrowIo = new TomorrowIo.ApiRequestMaker();

            Console.WriteLine("Use 'update' to get weather data, 'exit' to terminate the program");

            string? command;
            while ((command = Console.ReadLine()) != "exit")
            {
                if (command == "update")
                {
                    Console.WriteLine("Loading data...");

                    Weather? weather1 = OpenWeatherMap.OpenWeatherMapJsonParser.Parse(openWeatherMap.GetResponse());
                    Weather? weather2 = TomorrowIo.TomorrowIoJsonParser.Parse(tomorrowIo.GetResponse());

                    Console.Clear();
                    Console.WriteLine("**The data is current at " + DateTime.Now.ToString("HH:mm") + "**\n");

                    if (weather1 != null)
                    {
                        Console.WriteLine("Data from OpenWeatherMap:");
                        weather1.ShowWeather();
                    }
                    else
                    {
                        Console.WriteLine("!Сould not get data from the OpenWeatherMap!");
                        Console.WriteLine(openWeatherMap.accessError + "\n");
                    }

                    if (weather2 != null)
                    {
                        Console.WriteLine("Data from TomorrowIo:");
                        weather2.ShowWeather();
                    }
                    else
                    {
                        Console.WriteLine("!Сould not get data from the TomorrowIo!");
                        Console.WriteLine(tomorrowIo.accessError + "\n");
                    }
                }
                else if (command == "resetkeys")
                {
                    openWeatherMap.SetDefaultApiKey();
                    tomorrowIo.SetDefaultApiKey();

                    Console.WriteLine("api keys has been successfully changed\n");
                }
                else if (command == "changekey")
                {
                    Console.WriteLine("Use 1 to change the OpenWeatherMap api key, 2 to change the TomorrowIo api key, and anything else to cancel");
                    var input = Console.ReadLine();

                    if (input == "1" || input == "2")
                    {
                        Console.WriteLine("Enter new api key");
                        var newKey = Console.ReadLine();

                        if (input == "1")
                        {
                            openWeatherMap.ChangeApiKey(newKey);
                        }
                        else if (input == "2")
                        {
                            tomorrowIo.ChangeApiKey(newKey);
                        }

                        Console.WriteLine("api key has been successfully changed\n");
                    }
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
                Console.WriteLine("Use 'update' to update weather data, 'exit' to terminate the program,\n'changekey' to change api key or 'resetkeys' to set default keys");
            }
        }
    }
}