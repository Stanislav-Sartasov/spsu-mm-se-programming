using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parsers
{
    public abstract class JSONParser
    {

        protected string[] parsingParams;
        public IReadOnlyList<string> Headers { get; protected set; }

        protected WeatherInformation weatherInfo;

        protected CultureInfo local = new CultureInfo("ru-RU");
        public string Link { get; protected set; }

        public virtual WeatherInformation Parse(JObject json)
        {
            return null;
        }

        public WeatherInformation GetWeatherInfo()
        {
            return weatherInfo;
        }
    }
}
