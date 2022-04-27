using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonParsingLibrary;
using Newtonsoft.Json;
using System.Net;

namespace Task_2
{
    public class ConsoleWriter
    {
        private RequestURLGetter requestGetter;
        private TomorrowioMapper tomorrowMapper;
        private StormglassioMapper stormglassMapper;

        public Dictionary<string, string> Parameters { get; private set; }
        public string Answer { get; private set; }


        public ConsoleWriter()
        {
            requestGetter = new RequestURLGetter();
            tomorrowMapper = new TomorrowioMapper();
            stormglassMapper = new StormglassioMapper();
        }

        public string ShowSiteWeather(IWebServerHelper webParser, IResponseReader respReader)
        {
            Answer = "";
            if (FillParameters(webParser, respReader))
            {
                webParser.RequestURL = requestGetter.GetRequestURL(webParser.Site);
                Answer += string.Format("This information is from {0}\n", Parameters["site"]);
                Answer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n", Parameters["temperature"], Parameters["fahrenheitTemperature"]);
                Answer += string.Format("Humidity in percents - {0}\n", Parameters["humidity"]);
                Answer += string.Format("Cloud cover in percents - {0}\n", Parameters["cloudCover"]);
                Answer += string.Format("Wind speed in m/s - {0}\n", Parameters["windSpeed"]);
                Answer += string.Format("Wind direction in degrees - {0}\n\n", Parameters["windDirection"]);
                Console.WriteLine(Answer);
            }
            return Answer;
        }

        public bool FillParameters(IWebServerHelper webParser, IResponseReader respReader)
        {
            webParser.RequestURL = requestGetter.GetRequestURL(webParser.Site);
            if (webParser.MakeRequest())
            {
                respReader.Response = webParser.Response;
                string json = respReader.GetResponseInfo();
                if (webParser.Site == "tomorrow.io")
                {
                    TomorrowDataHolder tomorrowDataHolder = JsonConvert.DeserializeObject<TomorrowDataHolder>(json);
                    Parameters = tomorrowMapper.GetParameters(tomorrowDataHolder);
                }
                else
                {
                    StormGlassDataHolder stormglassDataHolder = JsonConvert.DeserializeObject<StormGlassDataHolder>(json);
                    Parameters = stormglassMapper.GetParameters(stormglassDataHolder);
                }
                return true;
            }
            return false;
        }
    }
}
