using System.Net;

namespace Requests
{
    public class GetRequest
    {
        private HttpWebRequest request;
        private WebProxy proxy;
        private string url;
        private string userAgent;
        public bool Connect { get; private set; }
        public string Accept { get; private set; }
        public string Host { get; private set; }
        public string Referer { get; private set; }
        public string Response { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }

        public GetRequest(string address, string accept, string host, string referer)
        {
            url = address;
            userAgent = "Mozilla/5.0(Windows NT 10.0; WOW64) AppleWebKit/537.36(KHTML, like Gecko) Chrome/98.0.4758.119 YaBrowser/22.3.0.2434 Yowser/2.5 Safari/537.36";
            proxy = new WebProxy("127.0.0.1:8888");
            Headers = new Dictionary<string, string>();
            Accept = accept;
            Host = host;
            Referer = referer;
        }

        public void Run()
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.CookieContainer = new CookieContainer();
            request.Proxy = proxy;
            request.UserAgent = userAgent;
            request.Accept = Accept;
            request.Host = Host;
            request.Referer = Referer;

            foreach (var pair in Headers)
            {
                request.Headers.Add(pair.Key, pair.Value);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    Response = new StreamReader(stream).ReadToEnd();
                }
                Connect = true;
            }
            catch(Exception ex)
            {
                Connect = false;
                //Console.WriteLine("Can't connect to site.");
            }
        }

        public void AddToHeaders(string key, string value)
        {
            Headers.Add(key, value);
        }
    }
}