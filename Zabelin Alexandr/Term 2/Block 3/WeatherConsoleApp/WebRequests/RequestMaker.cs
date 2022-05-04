using System.Net;
using System.Text;

namespace WebRequests
{
    public class RequestMaker : IRequestMaker
    {
        public string GetJSON(string url)
        {
            string recievedJSON;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.ContentType = "application/json";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);

                recievedJSON = streamReader.ReadToEnd();

                response.Close();
                streamReader.Close();
            }
            catch (Exception)
            {
                recievedJSON = "";
            }

            return recievedJSON;
        }
    }
}
