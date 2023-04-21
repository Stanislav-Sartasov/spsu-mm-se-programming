using Newtonsoft.Json.Linq;
using WeatherPattern;

namespace WeatherManagerAPI
{
    public class StormGlassIO : AManagerAPI
    {
        public StormGlassIO() : base("stormglass.io", $"https://api.stormglass.io/v2/weather/point?lat=59.93863&lng=30.31413&params=windDirection,windSpeed,airTemperature,cloudCover,humidity,precipitation&start={DateTime.Now.ToString("yyyy-MM-ddT00:00:00")}&end={DateTime.Now.ToString("yyyy-MM-ddT00:00:00")}&key={KeysAPI.stormGlassIOKey}")
        {

        }

        public override WeatherPtrn GetWeather(string response)
        {
            if (State == (response != null))
            {
                try
                {
                    JObject json = JObject.Parse(response);
                    JObject? data = (JObject?)json["hours"][0];
                    int cloudCover = (int)data["cloudCover"]["noaa"];
                    int humidity = (int)data["humidity"]["noaa"];
                    string precipitation = (string)data["precipitation"]["noaa"];
                    int temperature = (int)data["airTemperature"]["noaa"];
                    int windDirection = (int)data["windDirection"]["noaa"];
                    double windSpeed = (double)data["windSpeed"]["noaa"];

                    WeatherData = new WeatherPtrn(cloudCover, humidity, precipitation, temperature, windDirection, windSpeed);
                }
                catch (Exception e)
                {
                    State = false;
                    ExceptionMessage = e.Message;
                }
            }


            return WeatherData;
        }
    }
}
