using Model;

namespace Task5
{
    internal class Program
    {
        public static void Main()
        {
            Start();
        }

        private static void Start()
        {
            var info = new List<WeatherInfo>();
            TomorrowIoApi tomorrowIOApi = new(Config.TommorowIoApiConfig.lat, Config.TommorowIoApiConfig.lon, Config.TommorowIoApiConfig.api);
            OpenWeatherMapApi openWeatherMapApi = new(Config.OpenWeatherMapApiConfig.lat, Config.OpenWeatherMapApiConfig.lon, Config.OpenWeatherMapApiConfig.api);

            var openWeatherMapInfo = openWeatherMapApi.GetData();
            var tomorrowIOInfo = tomorrowIOApi.GetData();
            info.Add(tomorrowIOInfo);
            info.Add(openWeatherMapInfo);

            while (true)
            {
                Console.Clear();

                var greetNumber = Printer.Greetings();

                if (greetNumber == Choice.Exit)
                {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                if (greetNumber == Choice.OpenWeatherMap)
                {
                    try
                    {
                        string openWeatherMap = "OpenWeatherMap\n"; 
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 9) + (openWeatherMap.Length / 2)) + "}", openWeatherMap));
                        Printer.PrintWeather(openWeatherMapInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (greetNumber == Choice.TommorowIo)
                {
                    try
                    {
                        string tomorrowIO = "TomorrowIO\n";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 9) + (tomorrowIO.Length / 2)) + "}", tomorrowIO));
                        Printer.PrintWeather(tomorrowIOInfo);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (greetNumber == Choice.Both)
                {
                    foreach (var site in info)
                    {
                        try
                        {
                            Printer.PrintWeather(site);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}


        










