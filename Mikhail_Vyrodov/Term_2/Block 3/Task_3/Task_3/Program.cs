using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JsonParsingLibrary;
using System.Linq;


namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program shows current weather in St. Petersburg from stormglass.io and tomorrow.io");
            Console.WriteLine("If you want to get information from both sites - type 0");
            Console.WriteLine("From only tomorrow.io - type 1");
            Console.WriteLine("From only stormglass.io - type 2");
            string userChoice = Console.ReadLine();
            IoCContainer container = new IoCContainer();
            IWeatherDisplayer tomorrowWeatherDisplayer = null;
            IWeatherDisplayer stormglassWeatherDisplayer = null;
            if (userChoice == "1")
            {
                container.CreatingTomorrowio = true;
                tomorrowWeatherDisplayer = container.CreateDisplayer();
            }
            else if (userChoice == "2")
            {
                container.CreatingStormglassio = true;
                stormglassWeatherDisplayer = container.CreateDisplayer();
            }
            else
            {
                container.CreatingTomorrowio = true;
                tomorrowWeatherDisplayer = container.CreateDisplayer();
                container.CreatingStormglassio = true;
                container.CreatingTomorrowio = false;
                stormglassWeatherDisplayer = container.CreateDisplayer();

            }
            bool updateFlag = true;
            while (updateFlag)
            {
                if (userChoice == "1")
                {
                    tomorrowWeatherDisplayer.DisplayWeather();
                }
                else if (userChoice == "2")
                {
                    
                    stormglassWeatherDisplayer.DisplayWeather();
                }
                else
                {
                    tomorrowWeatherDisplayer.DisplayWeather();
                    stormglassWeatherDisplayer.DisplayWeather();
                }
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