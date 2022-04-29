using System;
using System.Text.Json.Serialization;

namespace OpenWeather
{
    public class Sys
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("sunrise")]
        public int Sunrize { get; set; }

        [JsonPropertyName("sunset")]
        public int Sunset { get; set; }

    }
}
