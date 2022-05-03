using System.Net;

namespace WeatherApp.ISite
{
    public class ApiRequestMaker
    {
        private string url;
        private string parameters;
        private string? key;
        public string? AccessError { get; private set; }

        public ApiRequestMaker(string url, string parameters, string? key)
        {
            this.url = url;
            this.parameters = parameters;
            this.key = key;
        }

        public string? GetResponse()
        {
            if (key == null)
            {
                AccessError = "Api access key not found";
                return null;
            }

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