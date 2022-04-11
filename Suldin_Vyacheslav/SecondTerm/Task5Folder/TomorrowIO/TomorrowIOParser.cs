using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Parsers;

namespace TomorrowIO
{
    public class TomorrowIOParser : JSONParser
    {
        public TomorrowIOParser()
        {
            parsingParams = "temperature,cloudCover,humidity,precipitationType,precipitationIntensity,windSpeed,windDirection".Split(",");
            Link = $"https://api.tomorrow.io/v4/timelines?location=59.873703,29.828038&fields={string.Join(",", parsingParams)}&timesteps=current&units=metric&apikey={Environment.GetEnvironmentVariable("TomorrowAPI")}";
            Headers = null;
            WeatherInfo = new WeatherInformation("TomorrowIO");
        }

        public override WeatherInformation Parse(JObject json)
        {
            WeatherInfo.Name = "TomorrowIO";

            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<TIORoot>(json.ToString());

                var values = root.data.TimeLines[0].Intervals[0].Values;
                WeatherInfo.MetricTemp = values.Temperature.ToString();
                WeatherInfo.ImperialTemp = Math.Round(values.Temperature * (9 / 5) + 32, 3).ToString();
                WeatherInfo.CloudCover = values.CloudCover.ToString();
                WeatherInfo.Humidity = values.Humidity.ToString();
                WeatherInfo.Precipipations = (PrecipitationType)(values.PrecipitationType) + ":" + values.PrecipitationIntensity.ToString();

                WeatherInfo.WindDegree = values.WindDirection.ToString();

                WeatherInfo.WindSpeed = values.WindSpeed.ToString();
            }
            else WeatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return this.WeatherInfo;
        }
    }
}
