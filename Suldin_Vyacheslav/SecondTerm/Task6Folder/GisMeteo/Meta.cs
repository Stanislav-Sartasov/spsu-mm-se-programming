using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class Meta
    {
        [JsonPropertyName("message")]
        public string Messege { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
