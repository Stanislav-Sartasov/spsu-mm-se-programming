using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Cloudiness
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }

        [JsonPropertyName("percent")]
        public int Percent { get; set; }
    }
}
