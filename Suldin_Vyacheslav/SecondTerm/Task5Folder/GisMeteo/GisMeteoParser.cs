using System;
using System.Collections.Generic;
using System.Globalization;
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
            weatherInfo = new WeatherInformation("GisMeteo");
        }

        public override WeatherInformation Parse(JObject json)
        {

            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<GMRoot>(json.ToString());
                var responce = root.Response;

                var asd = weatherInfo.ImperialTemp;
                weatherInfo.ImperialTemp = responce.Temperature.Air.F.ToString(local);
                weatherInfo.MetricTemp = responce.Temperature.Air.C.ToString(local);
                weatherInfo.CloudCover = responce.Cloudiness.Percent.ToString(local);
                weatherInfo.Humidity = responce.Humidity.Percent.ToString(local);
                weatherInfo.Precipipations = ((PrecipitationType)(responce.Precipitation.Type)).ToString() + ":" + responce.Precipitation.Intensity.ToString(local);
                weatherInfo.WindDegree = responce.Wind.Direction.Degree.ToString(local);
                weatherInfo.WindSpeed = responce.Wind.Speed.Meters.ToString(local);
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return weatherInfo;

        }
    }
}
