using System.Net;

namespace APIManagerTools
{
    public static class APIManager
    {
        public static (string, Exception?) GetResponse(string url)
        {
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;
            string response, errorMessage = "Failed to create a web request at the specified URL, check its correctness.";

            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                errorMessage = "The site is unavailable for some reason or cannot give response.";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                errorMessage = "Could not read data from the response stream from the server.";
                using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return (errorMessage, e);
            }

            return (response, null);
        }
    }
}