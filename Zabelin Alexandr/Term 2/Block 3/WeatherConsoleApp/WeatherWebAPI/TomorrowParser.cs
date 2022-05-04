using WebRequests;
using WeatherLibrary;
using Newtonsoft.Json.Linq;

namespace WeatherWebAPI
{
    public class TomorrowParser : AParser
    {
        private string apiKey = "Ywv1q9k7d5DCJNXpsuI3kDINtIXuG52G";

        private string sourceName = "TomorrowIO";
        private string startURL = "https://api.tomorrow.io/v4/timelines?";
        private string timesteps = "current";
        private string[] locationCoords = new string[] { "59.791891", "30.264067" };
        private string[] fields = new string[] { "temperature", "humidity", "windSpeed", "windDirection", "precipitationType", "cloudCover" };

        public override AWeather GetWeather()
        {
            IRequestMaker requestParser = new RequestMaker();
            string JSON = requestParser.GetJSON($"{startURL}&timesteps={timesteps}&fields={String.Join(",", fields)}&location={String.Join(",", locationCoords)}&apikey={apiKey}");

            return DeserializeToWeather(JSON);

        }

        public AWeather DeserializeToWeather(string JSON)
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
                var test = JObject.Parse(JSON).GetValue("data")["timelines"][0]["intervals"][0]["values"];
            }
            catch (Exception ex)
            {
                return new Weather(sourceName, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);
            }

            var data = JObject.Parse(JSON).GetValue("data")["timelines"][0]["intervals"][0]["values"];

            try
            {
                temperatureCelcius = data["temperature"].ToString();
            }
            catch (Exception e) { }

            try
            {
                cloudCover = TranslateCloudCover(data["cloudCover"].ToString());
            }
            catch (Exception e) { }

            try
            {
                humidity = data["humidity"].ToString();
            }
            catch (Exception e) { }

            try
            {
                precipitation = TranslatePrecipitation(data["precipitationType"].ToString());
            }
            catch (Exception e) { }

            try
            {
                windSpeed = data["windSpeed"].ToString();
            }
            catch (Exception e) { }

            try
            {
                windDirection = data["windDirection"].ToString();
            }
            catch (Exception e) { }


            return new Weather(sourceName, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);
        }

        private string TranslatePrecipitation(string? precipitation)
        {
            switch (precipitation)
            {
                case "0":
                    return "Sunny";

                case "1":
                    return "Rain";

                case "2":
                    return "Snow";

                case "3":
                    return "Freezing Rain";

                case "4":
                    return "Ice Pellets";

                default:
                    return DefaultValue;
            }
        }
    }
}
