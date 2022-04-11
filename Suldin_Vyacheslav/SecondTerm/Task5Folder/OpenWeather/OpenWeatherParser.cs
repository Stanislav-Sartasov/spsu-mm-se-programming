using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Parsers;

namespace OpenWeather
{
    public class OpenWeatherParser : JSONParser
    {
        public OpenWeatherParser()
        {
            Link = $"https://api.openweathermap.org/data/2.5/weather?lat=59.873703&lon=29.828038&appid={Environment.GetEnvironmentVariable("OpenWeatherAPI")}&units=metric";
            Headers = null;
            WeatherInfo = new WeatherInformation("OpenWeather");
        }
        public override WeatherInformation Parse(JObject json)
        {
            WeatherInfo.Name = "OpenWeather";

            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<OWRoot>(json.ToString());

                WeatherInfo.MetricTemp = root.Main.Temperature.ToString();
                WeatherInfo.ImperialTemp = Math.Round(root.Main.Temperature * (9 / 5) + 32, 3).ToString();
                WeatherInfo.CloudCover = root.Clouds.All.ToString();
                WeatherInfo.Humidity = root.Main.Humidity.ToString();

                if (root.Rain != null)
                    WeatherInfo.Precipipations = PrecipitationType.Rain.ToString() + ":" + root.Rain.ToString();
                else if (root.Snow != null)
                    WeatherInfo.Precipipations = PrecipitationType.Snow.ToString() + ":" + root.Snow.ToString();
                else
                    WeatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();

                WeatherInfo.WindDegree = root.Wind.Degree.ToString();

                WeatherInfo.WindSpeed = root.Wind.Speed.ToString();
            }
            else WeatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return this.WeatherInfo;
        }
    }
}
