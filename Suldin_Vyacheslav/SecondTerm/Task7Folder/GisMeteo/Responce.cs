using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Response
    {
        [JsonPropertyName("precipitation")]
        public Precipitation Precipitation { get; set; }

        [JsonPropertyName("pressure")]
        public Pressure Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public Humidity Humidity { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("gm")]
        public int Gm { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("cloudiness")]
        public Cloudiness Cloudiness { get; set; }

        [JsonPropertyName("date")]
        public Date Date { get; set; }

        [JsonPropertyName("radiation")]
        public Radiation Radiation { get; set; }

        [JsonPropertyName("city")]
        public int City { get; set; }

        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("storm")]
        public bool Storm { get; set; }

        [JsonPropertyName("temperature")]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("description")]
        public Description Description { get; set; }
    }
}
