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
                lastTemperatureCelsius = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["main"].ToString())["temp"];
                lastTemperatureFahrenheit = lastTemperatureCelsius * 1.8f + 32f;
                lastHumidity = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["main"].ToString())["humidity"];
                lastWindDirection = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["wind"].ToString())["deg"];
                lastWindSpeed = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["wind"].ToString())["speed"];
                lastCloudCover = JsonConvert.DeserializeObject<IDictionary<string, float>>(res["clouds"].ToString())["all"];
            }
            return response.IsSuccessStatusCode;
        }
    }
}