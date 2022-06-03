using Newtonsoft.Json.Linq;
using WeatherTools;

namespace Sites
{
    public class OpenWeatherMap : ASite
    {
        public OpenWeatherMap() : base("openweathermap.org", "https://api.openweathermap.org/data/2.5/weather?id=498817&units=metric&lang=en&appid=b2f3a2c14cb241b45d8dd2adb52381e2")
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
                    double temperatureInCelsius = (double)json["main"]["temp"];
                    int cloudCover = (int)json["clouds"]["all"];
                    int humidity = (int)json["main"]["humidity"];
                    string precipitation = "Precipitation: " + json["weather"][0]["main"].ToString() + "\nDescription: " + json["weather"][0]["description"].ToString();
                    int windDirection = (int)json["wind"]["deg"];
                    double windSpeed = (double)json["wind"]["speed"];

                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddSeconds((double)json["dt"]).ToLocalTime();

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