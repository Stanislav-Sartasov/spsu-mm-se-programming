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
                LastTemperatureCelsius = GetJsonValue(result, "airTemperature");
                LastTemperatureFahrenheit = LastTemperatureCelsius * 1.8f + 32f;
                LastCloudCover = GetJsonValue(result, "cloudCover");
                LastPrecipitation = GetJsonValue(result, "precipitation");
                LastHumidity = GetJsonValue(result, "humidity");
                LastWindDirection = GetJsonValue(result, "windDirection");
                LastWindSpeed = GetJsonValue(result, "windSpeed");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}