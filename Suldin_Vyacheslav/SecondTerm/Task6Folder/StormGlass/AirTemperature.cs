using System;
using System.Text.Json.Serialization;

namespace StormGlass
{
    public class AirTemperature
    {

        [JsonPropertyName("noaa")]
        public float Noaa { get; set; }

        [JsonPropertyName("sg")]
        public float Sg { get; set; }
    }
}
