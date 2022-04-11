using System;
using System.Text.Json.Serialization;

namespace TomorrowIO
{

    public class TIORoot
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

}
