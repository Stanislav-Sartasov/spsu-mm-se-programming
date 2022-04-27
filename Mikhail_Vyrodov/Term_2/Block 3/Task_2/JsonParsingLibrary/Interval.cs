using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class Interval
    {
        [JsonProperty("startTime")]
        public string StartTime { get; set; }
        [JsonProperty("values")]
        public Dictionary<string, string> Values { get; set; }
    }
}
