using System;
using System.IO;
using System.Net;
using WeatherUI_Winforms.Model;

namespace WeatherUI_WPF.Service
{
    public abstract class Api : IApi
    {
        public abstract string ApiName { get; }
        public abstract WeatherInfo GetData();

        public string WeatherForecast { get; private set; }

        public bool ConnectToService(string addressApi)
        {
            try
            {
                Console.WriteLine(ApiName);

                using (WebClient webClient = new WebClient())
                {
                    Stream strm = webClient.OpenRead(addressApi);
                    StreamReader sr = new StreamReader(strm);
                    WeatherForecast = sr.ReadToEnd();
                }
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}