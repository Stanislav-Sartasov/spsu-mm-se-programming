using WeatherServiceLib;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StormGlassWeatherServiceLib
{
    public class StormGlassWeatherService : AbstractWeatherService
    {
        private readonly HttpClient client;
        private readonly string urlParameters;
        public StormGlassWeatherService(double lat, double lon, string key) : base(lat, lon)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.stormglass.io/v2/weather/point");
            urlParameters = "?lat=" + lat.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture) + "&lng=" + lon.ToString("0.0000", System.Globalization.CultureInfo.InvariantCulture) + "&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed&source=sg";
            client.DefaultRequestHeaders.Add("Authorization", key);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private float? GetJsonValue(string result, string value)
        {
            try
            {
                var res = JsonConvert.DeserializeObject<IDictionary<string, object>>(result);
                var array = JsonConvert.DeserializeObject<IList<IDictionary<string, object>>>(res["hours"].ToString());
                return JsonConvert.DeserializeObject<IDictionary<string, float>>(array[11][value].ToString())["sg"];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                return null;
            }
        }

        public override bool UpdateInfo()
        {
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                lastTemperatureCelsius = GetJsonValue(result, "airTemperature");
                lastTemperatureFahrenheit = lastTemperatureCelsius * 1.8f + 32f;
                lastCloudCover = GetJsonValue(result, "cloudCover");
                lastPrecipitation = GetJsonValue(result, "precipitation");
                lastHumidity = GetJsonValue(result, "humidity");
                lastWindDirection = GetJsonValue(result, "windDirection");
                lastWindSpeed = GetJsonValue(result, "windSpeed");
            }
            return response.IsSuccessStatusCode;
        }
    }
}