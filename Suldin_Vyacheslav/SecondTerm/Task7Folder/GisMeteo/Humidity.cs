using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Humidity
    {
        [JsonPropertyName("percent")]
        public int Percent { get; set; }
    }
}
