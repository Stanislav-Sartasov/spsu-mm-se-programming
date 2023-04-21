using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class TomorrowDataHolder
    {
        [JsonProperty("data")]
        public TomorrowData Data { get; set; }
    }
}
