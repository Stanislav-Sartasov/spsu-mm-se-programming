using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Radiation
    {
        [JsonPropertyName("uvb_index")]
        public object UVBIndex { get; set; }

        public object UVB { get; set; }
    }
}
