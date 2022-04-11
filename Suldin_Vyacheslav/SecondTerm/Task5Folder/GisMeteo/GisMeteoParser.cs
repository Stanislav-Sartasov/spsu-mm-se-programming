using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Parsers;

namespace GisMeteo
{
    public class GisMeteoParser : JSONParser
    {
        public GisMeteoParser()
        {
            parsingParams = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection".Split(",");
            Link = "https://api.gismeteo.net/v2/weather/current/?latitude=59.873703&longitude=29.828038";
            Headers = new string[1] { $"X-Gismeteo-Token: { Environment.GetEnvironmentVariable("GisMeteoAPI")}" };
            WeatherInfo = new WeatherInformation("GisMeteo");
        }

        public override WeatherInformation Parse(JObject json)
        {
            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<GMRoot>(json.ToString());
                var responce = root.Response;

                WeatherInfo.ImperialTemp = responce.Temperature.Air.F.ToString();
                WeatherInfo.MetricTemp = responce.Temperature.Air.C.ToString();
                WeatherInfo.CloudCover = responce.Cloudiness.Percent.ToString();
                WeatherInfo.Humidity = responce.Humidity.Percent.ToString();
                WeatherInfo.Precipipations = ((PrecipitationType)(responce.Precipitation.Type)).ToString() + ":" + responce.Precipitation.Intensity.ToString();
                WeatherInfo.WindDegree = responce.Wind.Direction.Degree.ToString();
                WeatherInfo.WindSpeed = responce.Wind.Speed.Meters.ToString();
            }
            else WeatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return WeatherInfo;

        }
    }
}
