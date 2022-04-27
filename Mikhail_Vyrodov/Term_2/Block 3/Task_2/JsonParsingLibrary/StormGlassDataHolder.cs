using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class StormGlassDataHolder
    {
        [JsonProperty("hours")]
        public List<Hour> Hours { get; set; }
        [JsonProperty("meta")]
        public Dictionary<string, object> Meta { get; set; }
    }
}
