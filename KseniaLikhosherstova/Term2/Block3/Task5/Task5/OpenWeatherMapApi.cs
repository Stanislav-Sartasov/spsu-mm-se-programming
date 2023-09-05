using Model;
using System.Text.Json.Nodes;

namespace Task5
{
    public class OpenWeatherMapApi : Api
    {
        public override string ApiName { get { return "OpenWeatherMap"; } }

        private readonly string addressApi; 
     

        public OpenWeatherMapApi(double latitude, double longitude, string apiKey)
        {
            addressApi = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";
        }

        public override WeatherInfo GetData()
        {
            if (!base.ConnectToService(addressApi))
            {
                throw new Exception($"Connection to '{ApiName}' failed. Please try again.");
            }
            var weatherForecast = Parse(WeatherForecast);
            return weatherForecast;
        }

        public WeatherInfo Parse(string data)
        {
            var jsonObject = JsonNode.Parse(data).AsObject();

            double tempC = (double)jsonObject["main"]["temp"];
            double tempF = Math.Round((tempC * 1.8) + 32);
            double humidity = (int)jsonObject["main"]["humidity"];
            double clouds = (double)jsonObject["clouds"]["all"];
            string precipitation = (string)jsonObject["weather"][0]["main"];
            double windSpeed = (double)jsonObject["wind"]["speed"];
            double windDirection = (double)jsonObject["wind"]["deg"];

            return new WeatherInfo(tempC, tempF, clouds, humidity, precipitation, windDirection, windSpeed);
        }
    }
}
