using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebLibrary;

namespace Parsers
{
    public class JSONParser : IRequestable
    {
        public IReadOnlyList<string> Headers { get; protected set; }

        protected WeatherInformation weatherInfo;

        protected CultureInfo local = new CultureInfo("ru-RU");
        public string Link { get; protected set; }

        public string Key { get; set; }

        public JSONParser()
        {
        }
        public virtual void Parse(JObject json)
        {
        }
        public WeatherInformation GetWeatherInfo()
        {
            return weatherInfo;
        }
        public string GetAddres()
        {
            return Link;
        }
        public IReadOnlyList<string> GetHeaders()
        {
            return Headers;
        }
    }
}
