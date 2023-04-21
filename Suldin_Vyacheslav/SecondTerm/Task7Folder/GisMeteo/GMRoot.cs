using System;
using System.Text.Json.Serialization;

namespace GisMeteo
{
    public class GMRoot
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("response")]
        public Response Response { get; set; }
    }
}
