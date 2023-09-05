using System;
using System.Text.Json.Nodes;
using WeatherUI_Winforms.Model;

namespace WeatherUI_WPF.Service
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

            return new WeatherInfo
            {
                TempC = tempC.ToString(),
                TempF = tempF.ToString(),
                CloudsPercent = clouds.ToString(),
                Humidity = humidity.ToString(),
                Precipitation = precipitation,
                WindSpeed = windSpeed.ToString(),
                WindDirection = GetWindDirection(windDirection)
            };
        }
        public string GetWindDirection(double? deg)
        {
            if (deg == null) return null;
            string north = "North";
            string northEast = "Northeast";
            string east = "East";
            string southEast = "Southeast";
            string south = "South";
            string southWest = "Southwest";
            string west = "West";
            string northWest = "Northwest";

            string direct = deg switch
            {
                >= 0 and <= 22.5 => north,
                > 22.5 and <= 67.5 => northEast,
                > 67.5 and <= 112.5 => east,
                > 112.5 and <= 157.5 => southEast,
                > 157.5 and <= 202.5 => south,
                > 202.5 and <= 247.5 => southWest,
                > 247.5 and <= 292.5 => west,
                > 292.5 and <= 337.5 => northWest,
                > 337.5 and <= 360 => north,
                _ => throw new ArgumentOutOfRangeException(nameof(deg))
            };
            return direct;
        }
    }
}