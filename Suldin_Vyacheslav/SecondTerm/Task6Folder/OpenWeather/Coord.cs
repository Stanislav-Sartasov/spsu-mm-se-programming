using System;
using System.Text.Json.Serialization;

namespace OpenWeather
{
    public class Coord
    {

        [JsonPropertyName("lon")]
        public float Lon { get; set; }

        [JsonPropertyName("lat")]
        public float Lat { get; set; }
    }
}
