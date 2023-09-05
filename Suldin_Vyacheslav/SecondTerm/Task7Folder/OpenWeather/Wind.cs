using System;
using System.Text.Json.Serialization;

namespace OpenWeather
{
    public class Wind
    {
        [JsonPropertyName("speed")]
        public float Speed { get; set; }

        [JsonPropertyName("deg")]
        public int Degree { get; set; }

        [JsonPropertyName("gust")]
        public float Gust { get; set; }
    }
}
