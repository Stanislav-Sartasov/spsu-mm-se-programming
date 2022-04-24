using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class StormGlassioDataHolder
    {
        [JsonProperty("hours")]
        public List<StormGlassioHour> Hours { get; set; }
        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }
    }
}
