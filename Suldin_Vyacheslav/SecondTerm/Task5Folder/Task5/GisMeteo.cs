using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class GisMeteo : WeatherRequest
    {
        public GisMeteo()
        {
            Site = "api.gismeteo.net";
            this.SetKey();
            this.TimeUpdate();
            Headers = new string[] { $"X-Gismeteo-Token: {Key}" };
            Params = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection";
        }
        protected override void SetAddress(Units unit)
        {
            Address = $"https://{Site}/v2/weather/current/?latitude={Latitude}&longitude={Longitude}";
        }   

        public override string[] GetInfo()
        {
            string[] answer = new string[7];

            SetAddress((Units)0);

            var json = GetJSON();

            if (json != null)
            {
                string[] fields = Params.Split(",");

                answer[0] = json["temperature"]["air"].ToString();
                answer[1] = ((Convert.ToDouble(answer[0]) * 9 / 5) + 32).ToString();
                answer[2] = json["cloudiness"]["percent"].ToString();
                answer[3] = json["humidity"]["percent"].ToString();
                var perpInfo = json["precipitation"];
                answer[4] = (PercType)(Convert.ToInt32(perpInfo["type"])) + perpInfo["intensity"].ToString() + "/3";
                answer[5] = json["wind"]["degree"].ToString();
                answer[6] = json["wind"]["speed"]["m_s"].ToString();

                return answer;
            }
            else return new string[] { "" };
        }
    }
}
