using WebRequests;
using WeatherLibrary;
using Newtonsoft.Json.Linq;

namespace WeatherWebAPI
{
    public class StormGlassParser : AParser
    {
        private string apiKey = "4d2e97d2-cafc-11ec-8f63-0242ac130002-4d2e9886-cafc-11ec-8f63-0242ac130002";

        private string sourceName = "StormGlass";
        private string startURL = "https://api.stormglass.io/v2/weather/point?";
        private string[] locationCoords = new string[] { "59.791891", "30.264067" };
        private string[] parameters = new string[] { "airTemperature", "cloudCover", "windDirection", "windSpeed", "humidity" };
        private string source = "noaa";

        public override Weather GetWeather()
        {
            long currentMoscowTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + 3 * 60 * 60;
            IRequestMaker requestParser = new RequestMaker();

            string? JSON = requestParser.GetJSON($"{startURL}lat={locationCoords[0]}&lng={locationCoords[1]}&params={String.Join(",", parameters)}&start={currentMoscowTime}&end={currentMoscowTime}&source={source}&key={apiKey}");

            return DeserializeToWeather(JSON);
        }

        public Weather DeserializeToWeather(string JSON)
        {
            string? temperatureCelcius = DefaultValue;
            string? temperatureFahrenheit = DefaultValue;
            string? cloudCover = DefaultValue;
            string? humidity = DefaultValue;
            string? precipitation = DefaultValue;
            string? windSpeed = DefaultValue;
            string? windDirection = DefaultValue;

            try
            {
                JObject.Parse(JSON).GetValue("hours");
            }
            catch (Exception e)
            {
                return new Weather(sourceName, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);
            }

            var data = JObject.Parse(JSON).GetValue("hours");

            try
            {
                temperatureCelcius = data[0]["airTemperature"][source].ToString();
            }
            catch (Exception e) { }

            try
            {
                cloudCover = TranslateCloudCover(data[0]["cloudCover"][source].ToString());
            }
            catch (Exception e) { }

            try
            {
                humidity = data[0]["humidity"][source].ToString();
            }
            catch (Exception e) { }

            try
            {
                windSpeed = data[0]["windSpeed"][source].ToString();
            }
            catch (Exception e) { }

            try
            {
                windDirection = data[0]["windDirection"][source].ToString();
            }
            catch (Exception e) { }


            return new Weather(sourceName, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);
        }
    }
}
