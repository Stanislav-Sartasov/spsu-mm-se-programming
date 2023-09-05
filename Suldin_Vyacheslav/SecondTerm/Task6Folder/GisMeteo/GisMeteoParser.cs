using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Parsers;

namespace GisMeteo
{
    public class GisMeteoParser : JSONParser
    {
        public GisMeteoParser(string key)
        {
            if (key == "") key += "null";
            this.Key = key;
            Link = "https://api.gismeteo.net/v2/weather/current/?latitude=59.873703&longitude=29.828038";
            Headers = new string[1] { $"X-Gismeteo-Token: { key }" };
            
        }
        public GisMeteoParser(JObject json)
        {
            weatherInfo = new WeatherInformation("GisMeteo");
            Parse(json);
        }

        public override void Parse(JObject json)
        {

            if (json["ERROR"] == null)
            {

                try
                {
                    var root = JsonSerializer.Deserialize<GMRoot>(json.ToString());
                    var responce = root.Response;
                    weatherInfo.ImperialTemp = responce.Temperature.Air.F.ToString(local);
                    weatherInfo.MetricTemp = responce.Temperature.Air.C.ToString(local);
                    weatherInfo.CloudCover = responce.Cloudiness.Percent.ToString(local);
                    weatherInfo.Humidity = responce.Humidity.Percent.ToString(local);
                    if (responce.Precipitation.Amount != null)
                        weatherInfo.Precipipations = ((PrecipitationType)(responce.Precipitation.Type)).ToString() + ":" + ((double)responce.Precipitation.Amount).ToString(local) ;
                    else weatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();
                    if (responce.Wind.Direction.Degree == null)
                        weatherInfo.WindDegree = "NoInfo";
                    else
                        weatherInfo.WindDegree = responce.Wind.Direction.Degree.ToString();
                    weatherInfo.WindSpeed = responce.Wind.Speed.Meters.ToString(local);
                    weatherInfo.Error = null;
                }
                catch (Exception)
                {
                    throw new Exception("GisMeteo changes his output");
                }
                
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];

        }
    }
}
