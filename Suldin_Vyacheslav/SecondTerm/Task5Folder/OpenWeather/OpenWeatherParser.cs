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

                weatherInfo.MetricTemp = root.Main.Temperature.ToString(local);
                weatherInfo.ImperialTemp = Math.Round(root.Main.Temperature * (9 / 5) + 32, 3).ToString(local);
                weatherInfo.CloudCover = root.Clouds.All.ToString(local);
                weatherInfo.Humidity = root.Main.Humidity.ToString(local);

                if (root.Rain != null)
                    weatherInfo.Precipipations = PrecipitationType.Rain.ToString() + ":" + root.Rain.OneHour.ToString(local);
                else if (root.Snow != null)
                    weatherInfo.Precipipations = PrecipitationType.Snow.ToString() + ":" + root.Snow.OneHour.ToString(local);
                else
                    weatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();

                weatherInfo.WindDegree = root.Wind.Degree.ToString(local);

                weatherInfo.WindSpeed = root.Wind.Speed.ToString(local);
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return this.weatherInfo;
        }
    }
}
