using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TomorrowIO
{
    public class Data
    {
        [JsonPropertyName("timelines")]
        public TimeLine[] TimeLines { get; set; }
    }
}
