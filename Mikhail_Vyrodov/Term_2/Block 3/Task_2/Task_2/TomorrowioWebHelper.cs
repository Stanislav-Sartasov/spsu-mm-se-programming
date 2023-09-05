using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class TomorrowioWebHelper : IWebServerHelper
    {
        public Uri RequestURL { get; set; }
        public HttpWebResponse Response { get; private set; }

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
                Console.WriteLine("stormglass.io is currently unavailable");
                return false;
            }
        }
    }
}
