using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Description
    {
        [JsonPropertyName("full")]
        public string Full { get; set; }
    }
}
