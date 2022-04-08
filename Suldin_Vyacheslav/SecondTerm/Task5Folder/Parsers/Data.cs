using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsers
{
    public class Data
    {
        private string[] coord = new string[2] { "59.873703", "29.828038" };

        public string Link { get; private set; }
        public IReadOnlyList<string> Headers { get; private set; }

        public Data(Type type)
        {
            if (type == typeof(TomorrowIOParser))
            {
                Link = $"https://api.tomorrow.io/v4/timelines?location={coord[0]},{coord[1]}&fields=temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection&timesteps=current&units=metric&apikey={Environment.GetEnvironmentVariable("TomorrowAPI")}";
                Headers = null;
            }
            else if (type == typeof(OpenWeatherParser))
            {
                Link = $"https://api.openweathermap.org/data/2.5/weather?lat={coord[0]}&lon={coord[1]}&appid={Environment.GetEnvironmentVariable("OpenWeatherAPI")}&units=metric";
                Headers = null;
            }
            else if (type == typeof(StormGlassParser))
            {
                Link = $"https://api.stormglass.io/v2/weather/point?lat={coord[0]}&lng={coord[1]}&params=airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}";
                Headers = new string[1] { $"Authorization: { Environment.GetEnvironmentVariable("StormGlassAPI")}" };
            }
        }
    }
}
