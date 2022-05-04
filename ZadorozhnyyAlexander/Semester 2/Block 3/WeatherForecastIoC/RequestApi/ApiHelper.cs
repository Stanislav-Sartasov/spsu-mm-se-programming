using System.Net.Http.Headers;

namespace RequestApi
{
    public class ApiHelper
    {
        protected const string saintPetesbergLat = "59.93863";
        protected const string saintPetesbergLon = "30.31413";

        public HttpClient ApiClient { get; }
        public string UrlParameters { get; }

        public ApiHelper(string key, int type)
        {
            ApiClient = new HttpClient();
            
            if (type == 0)
            {
                ApiClient.BaseAddress = new Uri("https://api.stormglass.io/v2/weather/point");
                UrlParameters = String.Join("", CreateStormglassParams());
                ApiClient.DefaultRequestHeaders.Add("Authorization", key);
            }
            else if (type == 1)
            {
                ApiClient.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
                UrlParameters = String.Join("", CreateOpenweatherParams(key));
            }

            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ApiHelper(string[] parameters, string key, string url, int type, HttpClient client)
        {
            ApiClient = client;
            ApiClient.BaseAddress = new Uri(url);
            UrlParameters = String.Join("", parameters);
            if (type == 0)
                ApiClient.DefaultRequestHeaders.Add("Authorization", key);
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string[] CreateOpenweatherParams(string key)
        {
            string[] result = {
                $"?lat={saintPetesbergLat}",
                $"&lon={saintPetesbergLon}",
                $"&appid={key}",
                "&units=metric"
            };
            return result;
        }

        private string[] CreateStormglassParams()
        {
            string[] result = {
                $"?lat={saintPetesbergLat}",
                $"&lng={saintPetesbergLon}",
                "&params=",
                "airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed",
                $"&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                $"&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                "&source=sg",
            };
            return result;
        }
    }
}