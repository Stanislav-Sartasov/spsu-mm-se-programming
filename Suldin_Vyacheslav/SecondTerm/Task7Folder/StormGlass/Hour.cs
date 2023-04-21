using System;
using System.Text.Json.Serialization;

namespace StormGlass
{
    public class Hour
    {
        [JsonPropertyName("airTemperature")]
        public AirTemperature AirTemperature { get; set; }

        [JsonPropertyName("cloudCover")]
        public CloudCover CloudCover { get; set; }

        [JsonPropertyName("humidity")]
        public Humidity Humidity { get; set; }

        [JsonPropertyName("precipitation")]
        public Precipitation Precipitation { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("windDirection")]
        public WindDirection WindDirection { get; set; }

        [JsonPropertyName("windSpeed")]
        public WindSpeed WindSpeed { get; set; }

    }
}
