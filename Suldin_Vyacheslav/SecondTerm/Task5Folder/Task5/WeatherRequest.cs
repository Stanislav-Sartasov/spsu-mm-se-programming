using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Task5
{
    public abstract class WeatherRequest
    {
        protected string Key;
        protected string Latitude = "59.873703";
        protected string Longitude = "29.828038";
        protected string Site;
        protected string Address;
        protected string[] Headers;
        protected string Params;
        protected long Time;

        public void TimeUpdate()
        {
            Time = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
        }


        public virtual JObject GetJSON()
        {

            var request = new GetRequest(this.Address, this.Headers);

            string errorCondition = request.Send();
            var response = request.ResponseAsString;
            if (response != null)
            {
                JObject json = JObject.Parse(response);
                return json;
            }
            else return JObject.Parse("{\"ERROR\":\"" + $"{Convert.ToInt32(Regex.Replace(errorCondition, @"[^\d]+", ""))}\"" + "}");
            
        }   

        public virtual void SetKey()
        {
            Console.WriteLine(this.Site + ": Enter your key");

            string replacement = Console.ReadLine();

            if (Address != null)
                Address = Address.Replace(Key, replacement);
            if (Headers != null)
                Headers[0] = Headers[0].Replace(Key, replacement);

            Key = replacement;
            
        }
        public virtual void SetAddress(Units unit)
        {
        }

        public virtual string[] GetInfo()
        {
            return null;
        }
    }
}
