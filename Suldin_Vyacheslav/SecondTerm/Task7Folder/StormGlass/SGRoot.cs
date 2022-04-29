using System;
using System.Text.Json.Serialization;

namespace StormGlass
{
    public class SGRoot
    {
        [JsonPropertyName("hours")]
        public Hour[] Hours { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }
    }
}