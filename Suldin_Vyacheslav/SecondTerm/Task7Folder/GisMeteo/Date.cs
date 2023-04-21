using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Date
    {
        public string UTC { get; set; }

        [JsonPropertyName("local")]
        public string Local { get; set; }

        [JsonPropertyName("time_zone_offset")]
        public int TimeZoneOffset { get; set; }

        [JsonPropertyName("hr_to_forecast")]
        public object HRToForecast { get; set; }

        [JsonPropertyName("unix")]
        public int Unix { get; set; }
    }
}
