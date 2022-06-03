using Newtonsoft.Json.Linq;
using WeatherLib.WeatherInfo;

namespace WeatherLib.Sites.TomorrowIo
{
    public static class TomorrowIoParser
    {
        public static void Parse(Weather weather, string? json)
        {
            if (json == null)
                return;

            JObject obj = JObject.Parse(json);
            JToken info = obj["data"]["timelines"][0]["intervals"][0]["values"];
            weather.TempInKelvin = (double)info["temperature"] + 273.15;
            weather.Humidity = (int)info["humidity"];
            weather.Cloudiness = (int)info["cloudCover"];
            weather.Precipitation = ((Precipitation)(int)info["precipitationType"]).ToString();
            weather.WindDegree = (int)info["windDirection"];
            weather.WindSpeed = (double)info["windSpeed"];
        }
    }
}