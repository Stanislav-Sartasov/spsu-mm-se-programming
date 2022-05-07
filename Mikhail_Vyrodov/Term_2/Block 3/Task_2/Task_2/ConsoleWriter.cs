using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class ConsoleWriter
    {
        public Dictionary<string, string> Parameters { get; set; }
        public string Answer { get; private set; }

        public string ShowSiteWeather()
        {
            Answer = "";
            Answer += string.Format("This information is from {0}\n", Parameters["site"]);
            Answer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n", Parameters["temperature"], Parameters["fahrenheitTemperature"]);
            Answer += string.Format("Humidity in percents - {0}\n", Parameters["humidity"]);
            Answer += string.Format("Cloud cover in percents - {0}\n", Parameters["cloudCover"]);
            Answer += string.Format("Wind speed in m/s - {0}\n", Parameters["windSpeed"]);
            Answer += string.Format("Wind direction in degrees - {0}\n\n", Parameters["windDirection"]);
            Console.WriteLine(Answer);
            return Answer;
        }
    }
}
