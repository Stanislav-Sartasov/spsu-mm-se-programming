using Newtonsoft.Json.Linq;
using WeatherTools;

namespace Sites
{
    public class StormGlassIO : ASite
    {
        public StormGlassIO() : base("stormglass.io", $"https://api.stormglass.io/v2/weather/point?lat=59.93863&lng=30.31413&params=windDirection,windSpeed,airTemperature,cloudCover,humidity,precipitation&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&key=690cf524-ca21-11ec-a8d3-0242ac130002-690cf5ce-ca21-11ec-a8d3-0242ac130002")
        {
        }

        public override bool Parse()
        {
            bool successToParsing = response is not null && requestSuccess;

            if (successToParsing)
            {
                try
                {
                    var json = JObject.Parse(response);
                    var data = json["hours"][0];
                    double temperatureInCelsius = (double)data["airTemperature"]["noaa"];
                    int cloudCover = (int)data["cloudCover"]["noaa"];
                    int humidity = (int)data["humidity"]["noaa"];
                    string precipitation = $"Precipitation intensity: {data["precipitation"]["noaa"]} mm/hr";
                    int windDirection = (int)data["windDirection"]["noaa"];
                    double windSpeed = (double)data["windSpeed"]["noaa"];

                    DateTime dateTime = DateTime.Parse(data["time"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    WeatherInfo = new Weather(temperatureInCelsius, cloudCover, humidity, precipitation, windDirection, windSpeed, dateTime);
                }
                catch (Exception e)
                {
                    ExceptionMessages = response + $"\nException from message parsing: {e.Message}";
                    successToParsing = false;
                }
            }

            return successToParsing;
        }
    }
}