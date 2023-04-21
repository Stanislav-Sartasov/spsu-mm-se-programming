using Newtonsoft.Json.Linq;
using WeatherTools;

namespace Sites
{
    public class TomorrowIO : ASite
    {
        public TomorrowIO() : base("tomorrow.io", "https://api.tomorrow.io/v4/timelines?location=59.93863,30.31413&timezone=Europe/Moscow&fields=temperature,cloudCover,humidity,precipitationType,precipitationIntensity,precipitationProbability,windDirection,windSpeed&timesteps=current&units=metric&apikey=n44aixkh3pA9CbRFzrF6I4mo3r58BgHY")
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
                    var data = json["data"]["timelines"][0]["intervals"][0];
                    double temperatureInCelsius = (double)data["values"]["temperature"];
                    int cloudCover = (int)data["values"]["cloudCover"];
                    int humidity = (int)data["values"]["humidity"];
                    string precipitation = "Precipitation type: " + ((PrecipitationIO)(int)data["values"]["precipitationType"]).ToString() + $", precipitation intensity: {data["values"]["precipitationIntensity"]} mm/hr, precipitation probability: {data["values"]["precipitationProbability"]} %";
                    int windDirection = (int)data["values"]["windDirection"];
                    double windSpeed = (double)data["values"]["windSpeed"];

                    DateTime dateTime = DateTime.Parse(data["startTime"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);

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
