using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WebLibrary;
using Parsers;

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

            var services = new List<JSONParser> { new TomorrowIOParser(), new OpenWeatherParser(), new StormGlassParser() };

            while (true)
            {
                Console.WriteLine("\n");
                if (Console.ReadLine() != "Show") break;

                foreach (var service in services)
                {
                    var link = new Data(service.GetType());
                    var gr = new GetRequest(link.Link,link.Headers);
                    var jg = new JsonGetter(gr);
                    var json = jg.GetJSON();
                    var info = service.Parse(json);
                    ConsoleWriter.WtireLines(info);
                    Console.Beep();
                }
            }
        }
    }
}
