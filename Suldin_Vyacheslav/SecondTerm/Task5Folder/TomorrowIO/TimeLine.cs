using System;
using System.Text.Json.Serialization;

namespace TomorrowIO
{
    public class TimeLine
    {

        [JsonPropertyName("timestep")]
        public string TimeStep { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("intervals")]
        public Interval[] Intervals { get; set; }
    }

}
