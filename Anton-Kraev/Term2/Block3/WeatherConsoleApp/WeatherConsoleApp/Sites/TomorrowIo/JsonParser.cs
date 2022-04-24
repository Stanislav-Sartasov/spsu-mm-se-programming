using Newtonsoft.Json.Linq;
using WeatherConsoleApp.WeatherInfo;

namespace WeatherConsoleApp.Sites.TomorrowIo
{
    public static class JsonParser
    {
        public static Weather? Parse(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            JObject obj = JObject.Parse(json);
            JToken weather = obj["data"]["timelines"][0]["intervals"][0]["values"];
            double temperature = (double)weather["temperature"] + 273.15;
            int humidity = (int)weather["humidity"];
            int cloudiness = (int)weather["cloudCover"];
            string precipitation = ((Precipitation)(int)weather["precipitationType"]).ToString();
            int windDegree = (int)weather["windDirection"];
            double windSpeed = (double)weather["windSpeed"];
            return new Weather(temperature, humidity, cloudiness, precipitation, windDegree, windSpeed);
        }
    }
}