using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;

namespace Task_2
{
    public class ConsoleWriter
    {
        private RequestURLGetter requestGetter;
        private JSONParser jsonParser;

        public Dictionary<string, string> Parameters { get; private set; }
        public string Answer { get; private set; }


        public ConsoleWriter()
        {
            jsonParser = new JSONParser();
            requestGetter = new RequestURLGetter();
        }

        public string ShowSiteWeather(IWebServerHelper webParser)
        {
            Answer = "";
            webParser.RequestURL = requestGetter.GetRequestURL(webParser.Site);
            if (webParser.MakeRequest())
            {
                jsonParser.Json = webParser.GetJSON();
                jsonParser.FillParameters(webParser.Site);
                Parameters = jsonParser.Parameters;
                Answer += string.Format("This information is from {0}\n", Parameters["site"]);
                Answer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n", Parameters["temperature"], Parameters["fahrenheitTemperature"]);
                Answer += string.Format("Humidity in percents - {0}\n", Parameters["humidity"]);
                Answer += string.Format("Cloud cover in percents - {0}\n", Parameters["cloudCover"]);
                Answer += string.Format("Wind speed in m/s - {0}\n", Parameters["windSpeed"]);
                Answer += string.Format("Wind direction in degrees - {0}\n\n", Parameters["windDirection"]);
                Console.WriteLine("This information is from {0}", Parameters["site"]);
                Console.WriteLine("Air temperature in Celsius - {0}, in Fahrenheits - {1}", Parameters["temperature"], Parameters["fahrenheitTemperature"]);
                Console.WriteLine("Humidity in percents - {0}", Parameters["humidity"]);
                Console.WriteLine("Cloud cover in percents - {0}", Parameters["cloudCover"]);
                Console.WriteLine("Wind speed in m/s - {0}", Parameters["windSpeed"]);
                Console.WriteLine("Wind direction in degrees - {0}", Parameters["windDirection"]);
                Console.WriteLine();
            }
            return Answer;
        }

    }
}
