using System;
using System.Net;

namespace Task_2
{
    class StormglassioWebHelper : IWebServerHelper
    {
        public Uri RequestURL { get; set; }
        public HttpWebResponse Response { get; private set; }
        public string Site { get; } = "stormglass.io";

        public bool MakeRequest()
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(RequestURL);
            request.Headers["Authorization"] = "d88345da-be27-11ec-a1b6-0242ac130002-d883465c-be27-11ec-a1b6-0242ac130002";
            try
            {
                Response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException)
            {
                Console.WriteLine("stormglass.io is currently unavailable");
                return false;
            }
        }
    }
}
