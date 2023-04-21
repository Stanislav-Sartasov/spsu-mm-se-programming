using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WebLibrary;
using OpenWeather;
using GisMeteo;
using StormGlass;
using Parsers;
using TomorrowIO;
using System.Threading;
using System.Globalization;

namespace Task5
{
   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This app shows current weather in Peterhof using web services\n" + "'Show'-> if you want get actual information\n");
            string[] tableInfo = new string[] { "Web Service", "Temp(°C)", "Temp(°F)", "Cloud Cover(%)", "Humidity(%)", "Precip.(type:mm)", "Wind Speed(m/s)", "Wind Direction(°)" };
            foreach (string infoBar in tableInfo)
            {
                Console.Write(infoBar);
                ConsoleWriter.Fill(infoBar.Length);
            }

            var services = new List<JSONParser> { new TomorrowIOParser(),
                new OpenWeatherParser(),
                new StormGlassParser(),
                new GisMeteoParser() };

            while (true)
            {
                Console.WriteLine("\n");
                if (Console.ReadLine() != "Show") break;


                foreach (var service in services)
                {
                    var gr = new GetRequest(service.Link, service.Headers);
                    var jg = new JsonGetter(gr);
                    var json = jg.GetJSON();
                    var info = service.Parse(json);
                    ConsoleWriter.ShowWeatherInfo(info);
                    Console.Beep();
                }
            }
        }
    }
}
