using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace JsonParsingLibrary
{
    public class JSONParser
    {
        public string Json { get; set; }
        public Dictionary<string, string> Parameters { get; private set; }

        public void FillParameters(string site)
        {
            Parameters = new Dictionary<string, string>();
            if (site == "tomorrow.io")
            {
                TomorrowDataHolder tomorrowDataHolder = JsonConvert.DeserializeObject<TomorrowDataHolder>(Json);
                Dictionary<string, string> tempDict = tomorrowDataHolder.Data.TimeLines[0].Intervals[0].Values;
                foreach (var field in tempDict)
                {
                    if (field.Key == "precipitationIntensity")
                    {
                        Parameters.Add("precipitation", field.Value);
                    }
                    else
                        Parameters.Add(field.Key, field.Value);
                }
                Parameters.Add("site", site);
                string temperatureStr = Parameters["temperature"];
                temperatureStr = temperatureStr.Replace('.', ',');
                double temperature = Convert.ToDouble(temperatureStr);
                double fahrenheitTemperature = temperature * (9 / 5) + 32;
                Parameters.Add("fahrenheitTemperature", fahrenheitTemperature.ToString("0.##"));
            }
            else
            {
                StormGlassDataHolder stormGlassDataHolder = JsonConvert.DeserializeObject<StormGlassDataHolder>(Json);
                Hour hour = stormGlassDataHolder.Hours[0];
                Parameters.Add("cloudCover", hour.CloudCover["noaa"]);
                Parameters.Add("temperature", hour.Temperature["noaa"]);
                Parameters.Add("humidity", hour.Humidity["noaa"]);
                Parameters.Add("precipitation", hour.Precipitation["noaa"]);
                Parameters.Add("windSpeed", hour.WindSpeed["noaa"]);
                Parameters.Add("windDirection", hour.WindDirection["noaa"]);
                Parameters.Add("site", site);
                string temperatureStr = Parameters["temperature"];
                temperatureStr = temperatureStr.Replace('.', ',');
                double temperature = Convert.ToDouble(temperatureStr);
                double fahrenheitTemperature = temperature * (9 / 5) + 32;
                Parameters.Add("fahrenheitTemperature", fahrenheitTemperature.ToString("0.##"));
            }
        }
    }
}
