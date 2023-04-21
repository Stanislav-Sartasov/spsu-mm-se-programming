using System;
using System.Text.Json.Serialization;

namespace TomorrowIO
{
    public class Interval
    {
        [JsonPropertyName("startTime")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("values")]
        public Values Values { get; set; }
    }


}
