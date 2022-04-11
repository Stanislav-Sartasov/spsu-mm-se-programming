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
            weatherInfo = new WeatherInformation("StormGlass");
        }

        public override WeatherInformation Parse(JObject json)
        {
            //string[] information = new string[8];
            //information[0] = "StormGlass";
            if (json["ERROR"] == null)
            {
                var root = JsonSerializer.Deserialize<SGRoot>(json.ToString());
                var hour = root.Hours[0];

                weatherInfo.MetricTemp = hour.AirTemperature.Noaa.ToString();
                weatherInfo.ImperialTemp = Math.Round(hour.AirTemperature.Noaa * (9 / 5) + 32, 3).ToString();
                weatherInfo.CloudCover = hour.CloudCover.Sg.ToString();
                weatherInfo.Humidity = hour.Humidity.Noaa.ToString();

                if (hour.Precipitation.Noaa == 0)
                    weatherInfo.Precipipations = PrecipitationType.NoPrecip.ToString();
                weatherInfo.Precipipations += ":" + hour.Precipitation.Noaa.ToString();
             
                weatherInfo.WindDegree = hour.WindDirection.Sg.ToString();

                weatherInfo.WindSpeed = hour.WindSpeed.Noaa.ToString();
            }
            else weatherInfo.Error = json["ERROR"].ToString() + this.ToString().Split(".")[1];
            return weatherInfo;
        }
    }
}
