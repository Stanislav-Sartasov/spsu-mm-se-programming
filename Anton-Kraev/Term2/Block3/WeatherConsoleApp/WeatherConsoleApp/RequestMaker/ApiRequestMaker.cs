using System.Net;

namespace WeatherConsoleApp.ApiRequestMaker
{
    public abstract class AbstractApiRequestMaker
    {
        protected string url;
        protected string parameters;
        protected string? key;
        public string? AccessError { get; private set; }

        public string? GetResponse()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + parameters + key);

            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                AccessError = null;
            }
            catch (Exception ex)
            {
                AccessError = ex.Message;
                return null;
            }

            string json;

            using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
            {
                json = streamReader.ReadToEnd();
            }

            return json;
        }
    }
}