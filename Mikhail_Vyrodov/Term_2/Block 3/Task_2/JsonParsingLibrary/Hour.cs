using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class Hour
    {
        [JsonProperty("airTemperature")]
        public Dictionary<string, string> Temperature { get; set; }
        [JsonProperty("cloudCover")]
        public Dictionary<string, string> CloudCover { get; set; }
        [JsonProperty("humidity")]
        public Dictionary<string, string> Humidity { get; set; }
        [JsonProperty("precipitation")]
        public Dictionary<string, string> Precipitation { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("windDirection")]
        public Dictionary<string, string> WindDirection { get; set; }
        [JsonProperty("windSpeed")]
        public Dictionary<string, string> WindSpeed { get; set; }
    }
}
