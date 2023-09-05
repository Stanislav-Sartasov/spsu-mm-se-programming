using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Parsers
{
    public class WeatherInformation
    {
        public WeatherInformation(string name) 
        {
            Name =  name;
        }
        public string Name { get; private set; }
        public string MetricTemp {get; set;}
        public string ImperialTemp { get; set; }
        public string CloudCover { get; set; }
        public string Humidity { get; set; }
        public string Precipipations { get; set; }
        public string WindSpeed { get; set; }
        public string WindDegree { get; set; }
        public string Error { get; set; }

    }
}
