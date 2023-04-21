using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClasses
{
    public class TomorrowioWebHelper : IWebServerHelper
    {
        public Uri RequestURL { get; set; }
        public HttpWebResponse Response { get; private set; }
        public string Answer { get; private set; }

        public bool MakeRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestURL);
            try
            {
                Response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch
            {
                Answer = "stormglass.io is currently unavailable";
                Console.WriteLine(Answer);
                return false;
            }
        }
    }
}
