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
            weatherInfo = new WeatherInformation("OpenWeather");
        }
        public override WeatherInformation Parse(JObject json)
        {
            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<OWRoot>(json.ToString());

                weatherInfo.MetricTemp = root.Main.Temperature.ToString();
                weatherInfo.ImperialTemp = Math.Round(root.Main.Temperature * (9 / 5) + 32, 3).ToString();
                weatherInfo.CloudCover = root.Clouds.All.ToString();
                weatherInfo.Humidity = root.Main.Humidity.ToString();

                if (root.Rain != null)
                    weatherInfo.Precipipations = PrecipitationType.Rain.ToString() + ":" + root.Rain.ToString();
                else if (root.Snow != null)
                    weatherInfo.Precipipations = PrecipitationType.Snow.ToString() + ":" + root.Snow.ToString();
                else
                    weatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();

                weatherInfo.WindDegree = root.Wind.Degree.ToString();

                weatherInfo.WindSpeed = root.Wind.Speed.ToString();
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return this.weatherInfo;
        }
    }
}
