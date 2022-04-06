using ISites;
using Requests;
using static System.Console;

namespace Sites
{
    public class TomorrowIo : ISite
    {
        readonly string url = "https://www.tomorrow.io/weather/hourly/";
        readonly string accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
        readonly string host = "www.tomorrow.io";
        readonly string referer = "https://www.tomorrow.io/";
        readonly List<List<string>> security = new List<List<string>>
        {
            new List<string> { "sec-ch-ua", "\"Not A;Brand\";v =\"99\", \"Chromium\";v=\"98\", \"Yandex\";v=\"22\"" },
            new List<string> { "sec-ch-ua-mobile", "?0" },
            new List<string> { "sec-ch-ua-platform", "\"Windows\"" },
            new List<string> { "Sec-Fetch-Dest", "document" },
            new List<string> { "Sec-Fetch-Mode", "navigate" },
            new List<string> { "Sec-Fetch-Site", "none" },
            new List<string> { "Sec-Fetch-User", "?1" },
            new List<string> { "Upgrade-Insecure-Requests", "1" }
        };
        private GetRequest request;
        readonly List<string> patternsForPasrsing = new List<string>
        {
            @"(?<=temperature.:)\d+\.\d+",
            @"(?<=class=.y00NXe.><div class=.TTgbLt.>)\d+(?=%)",
            @"(?<=div class=.TTgbLt.>)\d+(?=%)",
            @"\d+(?= km/h)",
            @"(?<=style=.transform.rotate.)\d+(?=.\d+deg)",
            "No data"
        };

        public TomorrowIo()
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
                WriteLine("Tomorrow.io is down.");
                return;
            }

            Weather.Weather weather = new Parser.Parser(request.Response).Parse(patternsForPasrsing);
            WriteLine("Tomorrow.io:");
            WriteLine($"Temp in C°: {weather.TempC}");
            WriteLine($"Temp in F°: {weather.TempF}");
            WriteLine($"Clouds in %: {weather.Clouds}");
            WriteLine($"Humidity in %: {weather.Humidity}");
            WriteLine($"Wind speed in km/h: {weather.WindSpeed}");
            WriteLine($"Wind degree in °: {Int32.Parse(weather.WindDegree) % 360}");
            WriteLine($"Fallout: {weather.FallOut}");
        }
    }
}