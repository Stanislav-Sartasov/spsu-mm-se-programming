using System.Net;

namespace WeatherConsoleApp.ApiRequestMaker
{
    public abstract class AbstractApiRequestMaker
    {
        protected string url;
        protected string parameters;
        protected string key;
        public string? accessError { get; private set; }

        public string? GetResponse()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + parameters + key);

            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                accessError = null;
            }
            catch (Exception ex)
            {
                accessError = ex.Message;
                return null;
            }

            string json;

            using (StreamReader streamReader = new(httpWebResponse.GetResponseStream()))
            {
                json = streamReader.ReadToEnd();
            }

            return json;
        }

        public void ChangeApiKey(string? key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                this.key = key;
        }

        public abstract void SetDefaultApiKey();
    }
}