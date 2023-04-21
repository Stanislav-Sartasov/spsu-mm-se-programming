using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Speed
    {
        [JsonPropertyName("km_h")]
        public int Kilometers { get; set; }

        [JsonPropertyName("m_s")]
        public int Meters { get; set; }

        [JsonPropertyName("mi_h")]
        public int Miles { get; set; }
    }
}
