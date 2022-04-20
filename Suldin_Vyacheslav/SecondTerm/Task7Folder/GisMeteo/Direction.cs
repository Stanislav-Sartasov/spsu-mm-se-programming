using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Direction
    {
        [JsonPropertyName("degree")]
        public object Degree { get; set; }

        [JsonPropertyName("scale_8")]
        public float Scale { get; set; }
    }
}
