using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsingLibrary
{
    public class TomorrowioMapper : IJsonMapper<TomorrowDataHolder>
    {
        public Dictionary<string, string> GetParameters(TomorrowDataHolder dataHolder)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Dictionary<string, string> tempDict = dataHolder.Data.TimeLines[0].Intervals[0].Values;
            foreach (var field in tempDict)
            {
                if (field.Key == "precipitationIntensity")
                {
                    parameters.Add("precipitation", field.Value);
                }
                else
                    parameters.Add(field.Key, field.Value);
            }
            parameters.Add("site", "tomorrow.io");
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
