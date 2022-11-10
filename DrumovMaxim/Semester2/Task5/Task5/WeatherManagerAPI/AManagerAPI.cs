using System.Net;
using WeatherPattern;

namespace WeatherManagerAPI 
{
    public abstract class AManagerAPI : IManagerAPI
    {
        public WeatherPtrn? WeatherData { get; set; }
        public string Name { get; private set; }
        public string WebAddress { get; set; }
        public bool State { get; set; }

        public AManagerAPI(string name, string webAddress)
        {
            Name = name;
            WebAddress = webAddress;
        }
        public string GetResponse(string url)
        {
            string response;

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            
                using(StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }

                State = true;
            }
            catch (Exception e)
            {
                State = false;
                throw e;
            }
            
            return response;
        }

        public abstract WeatherPtrn GetWeather(string response);
         
        public void EmptyPattern()
        {
            WeatherData = null;
            State = false;
        }

    }
}
