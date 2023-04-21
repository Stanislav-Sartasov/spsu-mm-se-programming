﻿using WeatherLib.ISite;
using WeatherLib.WeatherInfo;

namespace WeatherLib.Sites.OpenWeatherMap
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private ApiRequestMaker requestMaker;
        public Weather? CurrentWeather { get; set; }

        public OpenWeatherMapWeatherService()
        {
            var url = "https://api.openweathermap.org/data/2.5/weather?";
            var parameters = "lat=59.94&lon=30.24&appid=";
            var key = Environment.GetEnvironmentVariable("OpenWeatherMapApiKey") ?? "e2d900011a6c02deccc3e56e7534ed32";
            requestMaker = new ApiRequestMaker(url, parameters, key);
        }

        public void UpdateWeather()
        {
            OpenWeatherMapParser.Parse(CurrentWeather!, requestMaker.GetResponse());
        }
    }
}