using System;
using System.Collections.Generic;
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
        public WeatherInformation WeatherInfo { get; protected set; }
        public string Link { get; protected set; }

        public virtual WeatherInformation Parse(JObject json)
        {
            return null;
        }

        
    }
}
