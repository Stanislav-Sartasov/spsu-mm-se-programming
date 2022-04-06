﻿using ISites;
using Requests;
using static System.Console;

namespace Sites
{
    public class OpenWeatherMap : ISite
    {
        readonly string url = "https://openweathermap.org/data/2.5/onecall?lat=59.8944&lon=30.2642&units=metric&appid=439d4b804bc8187953eb36d2a8c26a02";
        readonly string accept = "*/*";
        readonly string host = "openweathermap.org";
        readonly string referer = "https://openweathermap.org/";
        readonly List<List<string>> security = new List<List<string>>
        {
            new List<string> { "sec-ch-ua", "\"Not A;Brand\";v =\"99\", \"Chromium\";v=\"98\", \"Yandex\";v=\"22\"" },
            new List<string> { "sec-ch-ua-mobile", "?0" },
            new List<string> { "sec-ch-ua-platform", "\"Windows\"" },
            new List<string> { "Sec-Fetch-Dest", "empty" },
            new List<string> { "Sec-Fetch-Mode", "cors" },
            new List<string> { "Sec-Fetch-Site", "same-origin" }
        };
        private GetRequest request;
        readonly List<string> patternsForPasrsing = new List<string>
        {
            @"(?<=temp.:)-?\d+\.\d+",
            @"(?<=clouds.:)\d+",
            @"(?<=humidity.:)\d+",
            @"(?<=wind_speed.:)\d+",
            @"(?<=wind_deg.:)\d+",
            @"(?<=description.:.)(\w+.){1,3}(?=.,.icon)"
        };

        public OpenWeatherMap()
        {
            request = new GetRequest(url, accept, host, referer);
            foreach (List<string> pair in security)
            {
                request.AddToHeaders(pair.First(), pair.Last());
            }
        }

        public void ShowWeather()
        {
            request.Run();
            if (!request.Connect)
            {
                WriteLine("Openweathermap is down.");
                return;
            }

            Weather.Weather weather = new Parser.Parser(request.Response).Parse(patternsForPasrsing);
            WriteLine("Openweathermap:");
            WriteLine($"Temp in C°: {weather.TempC}°");
            WriteLine($"Temp in F°: {weather.TempF}°");
            WriteLine($"Clouds in %: {weather.Clouds}");
            WriteLine($"Humidity in %: {weather.Humidity}");
            WriteLine($"Wind speed in m/s: {weather.WindSpeed}");
            WriteLine($"Wind degree in °: {(Int32.Parse(weather.WindDegree) + 180) % 360}");
            WriteLine($"Fallout: {weather.FallOut}");
        }
    }
}
