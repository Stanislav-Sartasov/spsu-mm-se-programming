using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonParsingLibrary
{
    public class StormglassioMapper : IJsonMapper<StormGlassDataHolder>
    {
        public Dictionary<string, string> GetParameters(StormGlassDataHolder dataHolder)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Hour hour = dataHolder.Hours[0];
            parameters.Add("cloudCover", hour.CloudCover["noaa"]);
            parameters.Add("temperature", hour.Temperature["noaa"]);
            parameters.Add("humidity", hour.Humidity["noaa"]);
            parameters.Add("precipitation", hour.Precipitation["noaa"]);
            parameters.Add("windSpeed", hour.WindSpeed["noaa"]);
            parameters.Add("windDirection", hour.WindDirection["noaa"]);
            parameters.Add("site", "stormglass.io");
            string temperatureStr = parameters["temperature"];
            temperatureStr = temperatureStr.Replace('.', ',');
            double temperature = Convert.ToDouble(temperatureStr);
            double fahrenheitTemperature = temperature * ((double)9 / (double)5) + 32;
            string strFTemperature = fahrenheitTemperature.ToString("0.##");
            strFTemperature = strFTemperature.Replace(',', '.');
            parameters.Add("fahrenheitTemperature", strFTemperature);
            return parameters;
        }
    }
}
