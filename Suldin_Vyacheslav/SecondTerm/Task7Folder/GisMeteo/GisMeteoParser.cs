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
        public GisMeteoParser(string token)
        {
            if (token == "") token += "null";
            key = token;
            parsingParams = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection".Split(",");
            Link = "https://api.gismeteo.net/v2/weather/current/?latitude=59.873703&longitude=29.828038";
            Headers = new string[1] { $"X-Gismeteo-Token: { key }" };
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
                if (responce.Wind.Direction.Degree == null)
                    weatherInfo.WindDegree = "NoInfo";
                else
                    weatherInfo.WindDegree = responce.Wind.Direction.Degree.ToString();
                weatherInfo.WindSpeed = responce.Wind.Speed.Meters.ToString(local);
                weatherInfo.Error = null;
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return weatherInfo;

        }
    }
}
