using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class TimeLine
    {
        [JsonProperty("timestep")]
        public string TimeStep { get; set; }
        [JsonProperty("endTime")]
        public string EndTime { get; set; }
        [JsonProperty("startTime")]
        public string StartTime { get; set; }
        [JsonProperty("intervals")]
        public List<Interval> Intervals { get; set; }
    }
}
