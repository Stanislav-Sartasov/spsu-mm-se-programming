using WeatherPattern;
using Newtonsoft.Json.Linq;

namespace WeatherManagerAPI
{
    public class TomorrowIO : AManagerAPI
    {
        public TomorrowIO() : base("tomorrow.io", $"https://api.tomorrow.io/v4/timelines?location=59.9386300,30.3141300&timezone=Europe/Moscow&fields=temperature,cloudCover,humidity,precipitationType,precipitationIntensity,precipitationProbability,windDirection,windSpeed&units=metric&apikey={KeysAPI.tomorrowIOKey}")
        {

        }

        public override WeatherPtrn GetWeather(string response)
        {
            if(State == (response != null))
            {
                try
                {
                    JObject json = JObject.Parse(response);
                    JObject? data = (JObject?)json["data"]["timelines"][0]["intervals"][0];
                    int cloudCover = (int)data["values"]["cloudCover"];
                    int humidity = (int)data["values"]["humidity"];
                    string precipType = ((PrecipitationTypes)(int)data["values"]["precipitationType"]).ToString();
                    string precipitation = $"type: {precipType}\nPrecipitation probability: {data["values"]["precipitationProbability"]} %\nPrecipitation intensity: {data["values"]["precipitationIntensity"]} ";
                    double temperature = (double)data["values"]["temperature"];
                    int windDirection = (int)data["values"]["windDirection"];
                    double windSpeed = (int)data["values"]["windSpeed"];

                    WeatherData = new WeatherPtrn(cloudCover, humidity, precipitation, temperature, windDirection, windSpeed);

                }
                catch (Exception e)
                {
                    State = false;
                    throw e;
                }
            }
            return WeatherData;
        }
    }
}
