using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Temperature
    {
        [JsonPropertyName("comfort")]
        public Comfort Comfort { get; set; }

        [JsonPropertyName("water")]
        public Water Water { get; set; }

        [JsonPropertyName("air")]
        public Air Air { get; set; }
    }
}
