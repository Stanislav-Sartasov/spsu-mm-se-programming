using System;
using System.Text.Json.Nodes;
using WeatherUI_WPF.Model;

namespace WeatherUI_WPF.Service
{
    public class TomorrowIOApi : Api
    {
        public override string ApiName { get { return "TomorrowIO"; } }

        private readonly string addressApi;

        public TomorrowIOApi(double latitude, double longitude, string apiKey)
        {
            var lat = latitude.ToString().Replace(',', '.');
            var lon = longitude.ToString().Replace(',', '.');

            addressApi = $"https://api.tomorrow.io/v4/timelines?location={lat},{lon}&timezone=Europe/Moscow&fields=temperature,cloudCover,humidity,precipitationType,windDirection,windSpeed&timesteps=current&units=metric&apikey={apiKey}";
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

            double tempC = (double)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["temperature"];
            double tempF = Math.Round((tempC * 1.8) + 32);
            double humidity = (int)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["humidity"];
            double clouds = (double)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["cloudCover"];
            int pr = (int)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["precipitationType"];
            string precipitation = GetPrecipitationType(pr);
            double windSpeed = (double)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["windSpeed"];
            double windDirection = (double)jsonObject["data"]["timelines"][0]["intervals"][0]["values"]["windDirection"];

            return new WeatherInfo
            {
                TempC = tempC,
                TempF = tempF,
                CloudsPercent = clouds,
                Humidity = humidity,
                Precipitation = precipitation,
                WindSpeed = windSpeed,
                WindDirection = GetWindDirection(windDirection)
            };
        }

        private string GetPrecipitationType(int? type)
        {
            if (type == null)
                return null;

            string noPrecipitation = "N/A";
            string rain = "Rain";
            string snow = "Snow";
            string freezingRain = "Freezing Rain";
            string icePellets = "Ice Pellets";

            string precipitation = type switch
            {
                0 => noPrecipitation,
                1 => rain,
                2 => snow,
                3 => freezingRain,
                4 => icePellets,
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
            return precipitation;
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
