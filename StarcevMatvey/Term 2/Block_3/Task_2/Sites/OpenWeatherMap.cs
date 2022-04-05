using ISites;
using Requests;

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
        readonly List<string> parametrsForPasrsing = new List<string>
        {
            "temp\":", ",",
            "clouds\":", ",",
            "humidity\":", ",",
            "wind_speed\"", ",",
            "wind_deg\"", ",",
            "description\":\"", "\""
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
                Console.WriteLine("Openweathermap is down.");
                return;
            }

            Weather.Weather weather = Parser.Parser.Parse(request.Response, parametrsForPasrsing);
            Console.WriteLine("Openweathermap:");
            Console.WriteLine($"Temp in C': {weather.TempC}");
            Console.WriteLine($"Temp in F': {weather.TempF}");
            Console.WriteLine($"Clouds: {weather.Clouds}");
            Console.WriteLine($"Humidity: {weather.Humidity}");
            Console.WriteLine($"Wind speed: {weather.WindSpeed}");
            Console.WriteLine($"Wind degree: {weather.WindDegree}");
            Console.WriteLine($"Fallout: {weather.FallOut}");
        }
    }
}
