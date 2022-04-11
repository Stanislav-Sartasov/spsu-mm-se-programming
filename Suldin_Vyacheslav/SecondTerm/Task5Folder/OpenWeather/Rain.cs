using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenWeather
{
    public class Rain
    {
        [JsonPropertyName("1h")]
        public double OneHour { get; set; }

        [JsonPropertyName("3h")]
        public double ThreeHours { get; set; }
    }
}
