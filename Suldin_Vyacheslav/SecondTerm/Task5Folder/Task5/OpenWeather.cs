using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class OpenWeather : WeatherRequest
    {
        public OpenWeather()
        {
            Site = "api.openweathermap.org";
            this.SetKey();
            this.TimeUpdate();
            Headers = null;
        }
        protected override void SetAddress(Units unit)
        {
            Address = $"https://{Site}/data/2.5/weather?lat={Latitude}&lon={Longitude}&appid={Key}&units={unit}";
        }

        public override string[] GetInfo()
        {
            string[] answer = new string[7];

            SetAddress((Units)1);

            var json = GetJSON();

            if (json != null)
            {
                answer[1] = json["main"]["temp"].ToString();
                answer[2] = json["clouds"]["all"].ToString();
                answer[3] = json["main"]["humidity"].ToString();
                if (json["snow"] != null) answer[4] = "snow:" + json["snow"]["1h"].ToString();
                else if (json["rain"] != null) answer[4] = "rain:" + json["rain"]["1h"].ToString();
                else answer[4] = "no per. info =(";

                answer[5] = json["wind"]["speed"].ToString();
                answer[6] = json["wind"]["deg"].ToString();


                
                Address = Address.Replace("imperial", "metric");

                json = GetJSON();

                if (json != null)
                    answer[0] = json["main"]["temp"].ToString();
                else
                    answer[0] = "???";  
                return answer;
            }
            else return new string[] { "" };
        }
    }
}
