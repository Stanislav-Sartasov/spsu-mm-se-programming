using System;
using Newtonsoft.Json.Linq;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

            string key1 = "vIO887d6L4mh5BlkFb8PstGtsCwX5FEd";
            string key2 = "c40d482621e3f80dc4635e29691f930a";
            string key3 = "de46590e-b3a9-11ec-8364-0242ac130002-de46597c-b3a9-11ec-8364-0242ac130002";

            string lat = "59.873703";
            string lon = "29.828038";
            string units = "metric";

            string[] headers = new string[] { $"Authorization:{key3}" };
            string fields3 = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection";
            string fields1 = "temperature,cloudCover,humidity,precipitationIntensity,precipitationType,windSpeed,windDirection";
            string url1 = $"https://api.tomorrow.io/v4/timelines?location={lat},{lon}&fields={fields1}&timesteps=current&units={units}&apikey={key1}";
            string url2 = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key2}&units={units}";
            string url3 = $"https://api.stormglass.io/v2/weather/point?lat={lat}&lng={lon}&params={fields3}&start={start}&end={start}";

            var request = new GetRequest(url2);

            request.Start();
            var response = request.ResponseAsString;
            var json = JObject.Parse(response);
        }
    }
}
