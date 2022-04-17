using System.Net.Http.Headers;
using Newtonsoft.Json;
using WeatherServiceLib;

namespace OpenWeatherMapServiceLib
{
    public class OpenWeatherMapService : AbstractWeatherService
    {
        private readonly HttpClient client;
        private readonly string urlParameters;
        public OpenWeatherMapService(double lat, double lon, string key) : base(lat, lon)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
            urlParameters = "?lat=" + lat.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture) +
                            "&lon=" + lon.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture) +
                            "&appid="+key+"&units=metric";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override bool UpdateInfo()
        {
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<IDictionary<string, object>>(result);
                LastTemperatureCelsius = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["main"].ToString())["temp"];
                LastTemperatureFahrenheit = LastTemperatureCelsius * 1.8f + 32f;
                LastHumidity = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["main"].ToString())["humidity"];
                LastWindDirection = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["wind"].ToString())["deg"];
                LastWindSpeed = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["wind"].ToString())["speed"];
                LastCloudCover = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["clouds"].ToString())["all"];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}