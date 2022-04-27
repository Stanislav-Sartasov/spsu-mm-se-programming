using ConsoleOutputManagement;
using Container;
using SiteInterface;
using Sites;

namespace Weather
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("The program shows the weather from 3 source\n");

            var sites = IoCContainer.GetSites(WeatherParameter.Current);

            while (true)
            {
                foreach (var site in sites)
                {
                    ConsoleOutput.WriteWeather(site.GetCityWeatherForecast());
                }

                Console.WriteLine("If you want to update data from sites, then write - 'Update'.\nIf you want to close the program, then write - 'Exit'.\n");

                var userAnswer = Console.ReadLine().ToLower().Replace(" ", "");

                while (userAnswer != "exit" && userAnswer != "update")
                {
                    Console.WriteLine("Wrong command. Try again, please.\nIf you want to update data from sites, then write - 'Update'.\n" +
                                      "If you want to close the program, then write - 'Exit'.");

                    userAnswer = Console.ReadLine().ToLower().Replace(" ", "");
                }

                if (userAnswer == "exit")
                {
                    break;
                }
            }

            Console.WriteLine("\nThat's all! Thank you!\n");
        }
    }
}

