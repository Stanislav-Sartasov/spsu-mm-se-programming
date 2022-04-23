using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program shows current weather in St. Petersburg from stormglass.io and tomorrow.io");
            StormglassioWebHelper stormglassparser = new StormglassioWebHelper();
            TomorrowioWebHelper tomorrowparser = new TomorrowioWebHelper();
            ConsoleWriter writer = new ConsoleWriter();
            bool updateFlag = true;
            while (updateFlag)
            {
                writer.ShowSiteWeather(tomorrowparser);
                writer.ShowSiteWeather(stormglassparser);
                Console.WriteLine("If you want to update information type Yes, if you don't want it type No");
                string answer = Console.ReadLine();
                if (answer == "No")
                {
                    updateFlag = false;
                }
                while (answer != "Yes" && answer != "No")
                {
                    Console.WriteLine("Please type Yes or No");
                    answer = Console.ReadLine();
                    if (answer == "No")
                    {
                        updateFlag = false;
                    }
                }
            }
        }
    }
}