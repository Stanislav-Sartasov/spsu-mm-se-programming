using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parsers
{
    public class StormGlassParser : JSONParser
    {
        public StormGlassParser()
        {
            parsingParams = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection".Split(",");
        }

        public override IReadOnlyList<string> Parse(JObject json)
        {
            string[] information = new string[8];
            information[0] = "StormGlass";
            if (json["ERROR"] == null)
            {
                int k = 2;
                foreach (string field in parsingParams)
                {
                    information[k] = json["hours"][0][$"{field}"]["noaa"].ToString();
                    if ($"{field}" == "precipitation" && information[k] == "0") information[k] = PrecipitationType.NoPrecip.ToString();
                    k++;
                }

                information[1] = Math.Round((Convert.ToDouble(information[2]) * 9 / 5 + 32), 3).ToString();

                ParsedInfo = information;
            }
            else ParsedInfo = new string[1] { json["ERROR"].ToString() + this.ToString().Split(".")[1].Split("Parser")[0] };
            return ParsedInfo;
        }
    }
}
