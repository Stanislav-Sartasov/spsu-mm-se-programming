using System;
using System.Text.Json.Serialization;

namespace TomorrowIO
{
    public class Values
    {

        [JsonPropertyName("cloudCover")]
        public int CloudCover { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("precipitationIntensity")]
        public float PrecipitationIntensity { get; set; }

        [JsonPropertyName("precipitationType")]
        public int PrecipitationType { get; set; }

        [JsonPropertyName("temperature")]
        public float Temperature { get; set; }

        [JsonPropertyName("windDirection")]
        public float WindDirection { get; set; }

        [JsonPropertyName("windSpeed")]
        public float WindSpeed { get; set; }
    }

}
