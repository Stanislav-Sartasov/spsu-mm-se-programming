using System.Net.Http.Headers;

namespace RequestApi
{
    public class ApiHelper
    {
        public HttpClient ApiClient { get; }
        public string UrlParameters { get; }
        
        public ApiHelper(string[] parameters, string key, string url, int type, HttpClient client)
        {
            ApiClient = client;
            ApiClient.BaseAddress = new Uri(url);
            UrlParameters = String.Join("", parameters);
            if (type == 0)
                ApiClient.DefaultRequestHeaders.Add("Authorization", key);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}