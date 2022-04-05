using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Task5
{
    public class WeatherRequest
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


        public JObject GetJSON()
        {
            
            var request = new GetRequest(this.Address, this.Headers);

            request.Send();
            var response = request.ResponseAsString;
            if (response != null)
            {
                JObject json = JObject.Parse(response);
                return json;
            }
            else return null;
            
        }   

        public void SetKey()
        {
            Console.WriteLine(this.Site + ": Enter your key");

            string replacement = Console.ReadLine();

            if (Address != null)
                Address = Address.Replace(Key, replacement);
            if (Headers != null)
                Headers[0] = Headers[0].Replace(Key, replacement);

            Key = replacement;
            
        }
        protected virtual void SetAddress(Units unit)
        {
        }

        public virtual string[] GetInfo()
        {
            return null;
        }
    }
}
