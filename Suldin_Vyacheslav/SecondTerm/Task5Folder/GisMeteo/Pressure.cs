using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Pressure
    {
        [JsonPropertyName("h_pa")]

        public int HPascal { get; set; }

        [JsonPropertyName("mm_hg_atm")]

        public int MillimeterHG { get; set; }

        [JsonPropertyName("in_hg")]
        public float InchHG { get; set; }
    }
}
