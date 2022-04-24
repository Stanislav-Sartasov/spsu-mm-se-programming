using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class TomorrowioData
    {
        [JsonProperty("timelines")]
        public List<TomorrowioTimeLine> TimeLines { get; set; }
    }
}
