using ISites;
using Requests;
using Tools;
using static System.Console;

namespace Sites
{
    public class TomorrowIo : ISite
    {
        public string Name { get; } = "TomorrowIo";
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
        private IGetRequest request;
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

        // only for testing
        public TomorrowIo(IGetRequest getRequest)
        {
            request = getRequest;
        }

        public Weather.Weather GetWeather()
        {
            request.Run();
            if (!request.Connect)
            {
                WriteLine("Tomorrow.io is down.");
                return null;
            }

            Weather.Weather weather = new Parser(request.Response).Parse(patternsForPasrsing);
            Weather.Weather weatherWithPara = new Weather.Weather(
                weather.TempC + "°C",
                weather.TempF + "°F",
                weather.Clouds + "%",
                weather.Humidity + "%",
                weather.WindSpeed + " km/h",
                weather.WindDegree != "No data" ? Int32.Parse(weather.WindDegree) % 360 + "°" : "No data",
                weather.FallOut);
            return weatherWithPara;
        }

        public void ShowWeather()
        {
            Painter painter = new Painter("Tomorrowio");
            Weather.Weather weather = GetWeather();
            painter.DrawWeather(weather);
        }
    }
}