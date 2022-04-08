using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WebLibrary;

namespace Parsers
{
    public class TomorrowIOParser : JSONParser
    {
        public TomorrowIOParser()
        {
            parsingParams = "temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection".Split(",");
        }

        public override IReadOnlyList<string> Parse(JObject json)
        {
            string[] information = new string[8];
            information[0] = "TomorrowIO";
            if (json["ERROR"] == null)
            {
                var values = json["data"]["timelines"][0]["intervals"][0]["values"];

                int k = 2;
                foreach (string field in parsingParams)
                {
                    if (field.Contains("precipitation"))
                    {
                        if (String.Equals(field, "precipitationType"))
                        {
                            information[k] = ((PrecipitationType)Convert.ToInt32(values[$"{field}"])).ToString();
                        }
                        else
                        {
                            information[k] += ":" + values[$"{field}"].ToString();
                            k++;
                        }
                    }
                    else
                    {
                        information[k] = values[$"{field}"].ToString();
                        k++;
                    }
                }
                information[1] = Math.Round((Convert.ToDouble(information[2]) * 9 / 5 + 32), 3).ToString();

                ParsedInfo = information;
            }
            else ParsedInfo = new string[1] { json["ERROR"].ToString() + this.ToString().Split(".")[1] };
            return ParsedInfo;
        }
    }
}
