using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Direction
    {
        [JsonPropertyName("degree")]
        public int Degree { get; set; }

        [JsonPropertyName("scale_8")]
        public int Scale { get; set; }
    }
}
