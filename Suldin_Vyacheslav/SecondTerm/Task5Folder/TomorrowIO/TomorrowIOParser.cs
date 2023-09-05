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
            weatherInfo = new WeatherInformation("TomorrowIO");
        }

        public override WeatherInformation Parse(JObject json)
        {
            if (json["ERROR"] == null)
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
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return this.weatherInfo;
        }
    }
}
