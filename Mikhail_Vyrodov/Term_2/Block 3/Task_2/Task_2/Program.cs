using System;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program shows current weather in St. Petersburg from stormglass.io and tomorrow.io");
            StormglassioWebHelper stormglassWebHelper = new StormglassioWebHelper();
            TomorrowioWebHelper tomorrowWebHelper = new TomorrowioWebHelper();
            ResponseReader respReader = new ResponseReader();
            WeatherDisplayer displayer = new WeatherDisplayer(tomorrowWebHelper, stormglassWebHelper);
            bool updateFlag = true;
            while (updateFlag)
            {
                displayer.DisplayTomorrowioWeather(respReader);
                displayer.DisplayStormglassioWeather(respReader);
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