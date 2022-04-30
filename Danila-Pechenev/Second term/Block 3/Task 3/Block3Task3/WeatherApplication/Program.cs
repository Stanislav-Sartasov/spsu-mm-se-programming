namespace WeatherApplication;
using IoC;

public class Program
{
    public static void Main(string[] args)
    {
        double[] locationCoordinates = new double[] { 59.939099, 30.315877 };
        string locationName = "St. Petersburg";

        string[] generalFields = 
            {"temperature", 
            "weather", 
            "cloudiness", 
            "humidity", 
            "wind direction", 
            "wind speed"};
        string[] units = { "", "", "%", "%", "deg", "m/s" };

        var weatherGettersFields = new Dictionary<string, string[]>();
        weatherGettersFields.Add("TomorrowIo", new string[] 
        {"temperature",
        "weatherCode",
        "cloudCover",
        "humidity",
        "windDirection",
        "windSpeed"
        });
        weatherGettersFields.Add("OpenWeatherMap", new string[]
        {
            "main.temp",
            "weather.main",
            "clouds.all",
            "main.humidity",
            "wind.deg",
            "wind.speed"
        });

        if (args.Length != 1)
        {
            Console.WriteLine("The program expects strictly one argument - the path to the file with API keys.");
        }
        else
        {
            Console.WriteLine("This application shows the weather in St. Petersburg now.");
            Console.WriteLine("The available (and default) services which we get data from are:");
            for (int i = 0; i < Container.UsedServices.Count - 1; i++)
            {
                Console.Write($"{Container.UsedServices[i]}, ");
            }

            if (Container.UsedServices.Count != 0)
            {
                Console.WriteLine($"{Container.UsedServices[^1]}.");
            }
            else
            {
                Console.WriteLine();
            }

            Console.WriteLine("If you want to add a new service, write \"add <service name>\"");
            Console.WriteLine("If you want to remove some service, write \"remove <service name>\"");
            Console.WriteLine("If you want to get the weather, just press Enter");
            Console.WriteLine("If you want to stop running the application, write \"stop\"");

            string userResponse = Console.ReadLine();
            while (userResponse != "")
            {
                if (userResponse.ToLower() == "stop")
                {
                    break;
                }

                if (userResponse.ToLower().StartsWith("add"))
                {
                    string[] words = userResponse.Split(' ');
                    if (words.Length >= 2)
                    {
                        bool success = Container.AddService(words[1]);
                        if (!success)
                        {
                            Console.WriteLine($"This service is not available!");
                        }
                    }
                }
                else if (userResponse.ToLower().StartsWith("remove"))
                {
                    string[] words = userResponse.Split(' ');
                    if (words.Length >= 2)
                    {
                        bool success = Container.RemoveService(words[1]);
                        if (!success)
                        {
                            Console.WriteLine($"This service is not in the list for use!");
                        }
                    }
                }

                Console.WriteLine("Current services which we get data from are:");
                for (int i = 0; i < Container.UsedServices.Count - 1; i++)
                {
                    Console.Write($"{Container.UsedServices[i]}, ");
                }

                if (Container.UsedServices.Count != 0)
                {
                    Console.WriteLine($"{Container.UsedServices[^1]}.");
                }
                else
                {
                    Console.WriteLine();
                }

                Console.WriteLine("If you want to add a new service, write \"add <service name>\"");
                Console.WriteLine("If you want to remove some service, write \"remove <service name>\"");
                Console.WriteLine("If you want to get the weather, just press Enter");
                Console.WriteLine("If you want to stop running the application, write \"stop\"");

                userResponse = Console.ReadLine();
            }

            if (userResponse == "")
            {
                var weatherGetters = Container.GetWeatherGetters(args[0]);
                var weatherWriter = new WeatherWriter(locationCoordinates, locationName, generalFields, units);
                weatherWriter.WriteWeatherManyTimes(weatherGetters, weatherGettersFields);
            }
        }
    }
}
