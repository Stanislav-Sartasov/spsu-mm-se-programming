using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parsers
{
    public class OpenWeatherParser : JSONParser
    {
        public override IReadOnlyList<string> Parse(JObject json)
        {
            string[] information = new string[8];

            information[0] = "OpenWeather";

            if (json["ERROR"] == null)
            {
                information[2] = json["main"]["temp"].ToString();
                information[3] = json["clouds"]["all"].ToString();
                information[4] = json["main"]["humidity"].ToString();
                if (json["snow"] != null) information[5] = PrecipitationType.Snow.ToString()+ ":" + json["snow"]["1h"].ToString();
                else if (json["rain"] != null) information[5] = PrecipitationType.Rain.ToString() + ":" + json["rain"]["1h"].ToString();
                else information[5] = PrecipitationType.NoPrecip.ToString();

                information[6] = json["wind"]["speed"].ToString();
                information[7] = json["wind"]["deg"].ToString();


                information[1] = Math.Round((Convert.ToDouble(information[2]) * 9 / 5 + 32), 3).ToString();
                ParsedInfo = information;
            }
            else ParsedInfo = new string[1] { json["ERROR"].ToString() + this.ToString().Split(".")[1] };
            return ParsedInfo;
        }
    }
}
