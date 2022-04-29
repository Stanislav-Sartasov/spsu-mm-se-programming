using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Wind
    {
        [JsonPropertyName("direction")]
        public Direction Direction { get; set; }

        [JsonPropertyName("speed")]
        public Speed Speed { get; set; }
    }
}
