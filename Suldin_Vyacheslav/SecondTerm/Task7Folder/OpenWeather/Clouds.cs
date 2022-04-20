using System;
using System.Text.Json.Serialization;

namespace OpenWeather
{
    public class Clouds
    {

        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}
