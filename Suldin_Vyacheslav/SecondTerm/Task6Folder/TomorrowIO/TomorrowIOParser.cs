using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Parsers;

namespace TomorrowIO
{
    public class TomorrowIOParser : JSONParser
    {
        public TomorrowIOParser(JObject json)
        {
            Parse(json);
        }
        public TomorrowIOParser(string key)
        {
            if (key == "") key += "null";
            this.Key = key;
            var parsingParams = "temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection";
            Link = $"https://api.tomorrow.io/v4/timelines?location=59.873703,29.828038&fields={parsingParams}&timesteps=current&units=metric&apikey={key}";
            Headers = null;
        }
        public override void Parse(JObject json)
        {
            weatherInfo = new WeatherInformation("TomorrowIO");

            if (json["ERROR"] == null)
            {
                try
                {
                    var root = JsonSerializer.Deserialize<TIORoot>(json.ToString());
                    var values = root.Data.TimeLines[0].Intervals[0].Values;
                    weatherInfo.MetricTemp = values.Temperature.ToString(local);
                    weatherInfo.ImperialTemp = Math.Round(values.Temperature * (9 / 5) + 32, 3).ToString(local);
                    weatherInfo.CloudCover = values.CloudCover.ToString(local);
                    weatherInfo.Humidity = values.Humidity.ToString(local);
                    weatherInfo.Precipipations = (PrecipitationType)(values.PrecipitationType) + ":" + values.PrecipitationIntensity.ToString(local);

                    weatherInfo.WindDegree = values.WindDirection.ToString(local);

                    weatherInfo.WindSpeed = values.WindSpeed.ToString(local);
                    weatherInfo.Error = null;
                }
                catch (Exception)
                {
                    throw new Exception("Tomorrow IO changes his output");
                }
                
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
        }
    }
}
