using Newtonsoft.Json.Linq;
using WeatherConsoleApp.WeatherInfo;

namespace WeatherConsoleApp.Sites.OpenWeatherMap
{
    public static class JsonParser
    {
        public static Weather? Parse(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            JObject weather = JObject.Parse(json);
            double temperature = (double) weather["main"]["temp"];
            int humidity = (int) weather["main"]["humidity"];
            int cloudiness = (int) weather["clouds"]["all"];
            string precipitation = weather["weather"][0]["main"].ToString();
            int windDegree = (int) weather["wind"]["deg"];
            double windSpeed = (double) weather["wind"]["speed"];
            return new Weather(temperature, humidity, cloudiness, precipitation, windDegree, windSpeed);
        }
    }
}