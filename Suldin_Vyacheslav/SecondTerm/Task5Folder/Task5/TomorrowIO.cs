using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace Task5
{
    public class TomorrowIO : WeatherRequest
    {
        public TomorrowIO()
        {
            Site = "api.tomorrow.io";
            Key = Environment.GetEnvironmentVariable("TomorrowAPI");
            this.TimeUpdate();
            Headers = null;
            Params = "temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection";
        }
        public override void SetAddress(Units unit)
        {
            Address = $"https://{Site}/v4/timelines?location={Latitude},{Longitude}&fields={Params}&timesteps=current&units={unit}&apikey={Key}";
        }

        public override string[] GetInfo()
        {
            string[] answer = new string[7];

            SetAddress((Units)1);

            var json = GetJSON();

            if (json["ERROR"] == null)
            {
                var values = json["data"]["timelines"][0]["intervals"][0]["values"];

                string[] fields = Params.Split(",");

                int k = 1;
                foreach (string field in fields)
                {
                    if (field.Contains("precipitation"))
                    {
                        if (String.Equals(field, "precipitationType"))
                        {
                            answer[k] = ((PrecipitationType)Convert.ToInt32(values[$"{field}"])).ToString();
                            
                        }
                        else
                        {
                            answer[k] += ":" + values[$"{field}"].ToString();
                            k++;
                        }
                    }
                    else
                    {
                        answer[k] = values[$"{field}"].ToString();
                        k++;
                    }
                }
                Address = Address.Replace("imperial", "metric");

                json = GetJSON();

                if (json != null) 
                    answer[0] = GetJSON()["data"]["timelines"][0]["intervals"][0]["values"]["temperature"].ToString();
                else
                    answer[0] = "???";
                return answer;
            }
            else return new string[1] { json["ERROR"].ToString() };
        }
    }
}
