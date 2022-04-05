using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    
    public class StormGlass : WeatherRequest
    {
        public StormGlass()
        {
            Site = "api.stormglass.io";
            this.SetKey();
            this.TimeUpdate();
            Headers = new string[] { $"Authorization:{Key}" };
            Params = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection";
        }
        protected override void SetAddress(Units unit)
        {
            Address = $"https://{Site}/v2/weather/point?lat={Latitude}&lng={Longitude}&params={Params}&start={Time}&end={Time}";
        }

        public override string[] GetInfo()
        {
            string[] answer = new string[7];

            SetAddress((Units)0);

            var json = GetJSON();

            if (json != null)
            {

                string[] fields = Params.Split(",");

                int k = 1;
                foreach (string field in fields)
                {
                    answer[k] = json["hours"][0][$"{field}"]["noaa"].ToString();
                    k++;
                }

                string temporary = ((Convert.ToDouble(answer[1]) * 9 / 5) + 32).ToString();

                answer[0] = answer[1];
                answer[1] = temporary;

                return answer;
            }
            else return new string[] { "" };
        }
    }
}
