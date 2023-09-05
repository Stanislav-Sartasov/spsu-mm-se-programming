using Newtonsoft.Json.Linq;
using WeatherLib.WeatherInfo;

namespace WeatherLib.Sites.OpenWeatherMap
{
    public static class OpenWeatherMapParser
    {
        public static void Parse(Weather weather, string? json)
        {
            if (json == null)
                return;

            JObject info = JObject.Parse(json);
            weather.TempInKelvin = (double)info["main"]["temp"];
            weather.Humidity = (int)info["main"]["humidity"];
            weather.Cloudiness = (int)info["clouds"]["all"];
            weather.Precipitation = info["weather"][0]["main"].ToString();
            weather.WindDegree = (int)info["wind"]["deg"];
            weather.WindSpeed = (double)info["wind"]["speed"];
        }
    }
}