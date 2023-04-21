using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Precipitation
    {
        [JsonPropertyName("type_ext")]
        public object TypeExt { get; set; }

        [JsonPropertyName("intensity")]
        public int Intensity { get; set; }

        [JsonPropertyName("correction")]
        public object Correction { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("type")]
        public int Type { get; set; }
    }
}
