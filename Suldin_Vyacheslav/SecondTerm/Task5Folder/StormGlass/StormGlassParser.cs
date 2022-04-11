using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Parsers;

namespace StormGlass
{
    public class StormGlassParser : JSONParser
    {
        public StormGlassParser()
        {
            parsingParams = "airTemperature,cloudCover,humidity,precipitation,windSpeed,windDirection".Split(",");
            Link = $"https://api.stormglass.io/v2/weather/point?lat=59.873703&lng=29.828038&params={string.Join(",", parsingParams)}&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}";
            Headers = new string[1] { $"Authorization: { Environment.GetEnvironmentVariable("StormGlassAPI")}" };
            WeatherInfo = new WeatherInformation("StormGlass");
        }

        public override WeatherInformation Parse(JObject json)
        {
            //string[] information = new string[8];
            //information[0] = "StormGlass";
            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<SGRoot>(json.ToString());
                var hour = root.Hours[0];

                WeatherInfo.MetricTemp = hour.AirTemperature.Noaa.ToString();
                WeatherInfo.ImperialTemp = Math.Round(hour.AirTemperature.Noaa * (9 / 5) + 32, 3).ToString();
                WeatherInfo.CloudCover = hour.CloudCover.Sg.ToString();
                WeatherInfo.Humidity = hour.Humidity.Noaa.ToString();

                if (hour.Precipitation.Noaa == 0)
                    WeatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();
                WeatherInfo.Precipipations += ":" + hour.Precipitation.Noaa.ToString();
             
                WeatherInfo.WindDegree = hour.WindDirection.Sg.ToString();

                WeatherInfo.WindSpeed = hour.WindSpeed.Noaa.ToString();
            }
            else WeatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return WeatherInfo;
        }
    }
}
