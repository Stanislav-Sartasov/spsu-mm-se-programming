using System;
using System.Text.Json.Serialization;

namespace StormGlass
{
    public class Meta
    {
        [JsonPropertyName("cost")]
        public int Cost { get; set; }

        [JsonPropertyName("dailyQuota")]
        public int DailyQuota { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }

        [JsonPropertyName("lat")]
        public float Lat { get; set; }

        [JsonPropertyName("lng")]
        public float Lng { get; set; }

        [JsonPropertyName("params")]
        public string[] Params { get; set; }

        [JsonPropertyName("requestCount")]
        public int RequestCount { get; set; }

        [JsonPropertyName("start")]
        public string Start { get; set; }

    }
}
