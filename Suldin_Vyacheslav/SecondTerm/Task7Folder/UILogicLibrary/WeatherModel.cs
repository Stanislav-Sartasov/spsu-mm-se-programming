using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parsers;
using StormGlass;
using WebLibrary;
using OpenWeather;
using GisMeteo;
using TomorrowIO;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UILogicLibrary
{
    public class WeatherModel
    {
        private ResourceManager rm = new ResourceManager("UILogicLibrary.BaseKeysSet", Assembly.GetExecutingAssembly());

        public List<JSONParser> services = new List<JSONParser>();
        public WeatherModel()
        {
            services = new List<JSONParser>() { new GisMeteoParser(rm.GetString("GisMeteoAPI")),
            new OpenWeatherParser(rm.GetString("OpenWeatherAPI")),
            new TomorrowIOParser(rm.GetString("TomorrowAPI")),
            new StormGlassParser(rm.GetString("StormGlassAPI"))};
        }

        public List<List<string>> GetWeather()
        {
            var list = new List<List<string>>();

            foreach (var service in services)
            {
                var subList = new List<string>();
                var gr = new GetRequest(service.Link, service.Headers);
                var jg = new JsonGetter(gr);
                var json = jg.GetJSON();
                var info = service.Parse(json);

                if (info.Error != null)
                {
                    subList.Add(info.Name + ":" + (ErrorType)Convert.ToInt32(Regex.Replace(info.Error, @"[^\d]+", "")));
                }
                else
                {
                    foreach (var margin in info.GetType().GetProperties().Where(x => x.Name != "Error"))
                    {
                        subList.Add(margin.GetValue(info).ToString());
                    }
                }
                list.Add(subList);
            }
            return list;
        }

        public string GetError(string term)
        {
            foreach (ErrorType error in Enum.GetValues(typeof(ErrorType)))
            {
                if (term.Contains(":") && term.Split(":")[1].Replace(" ", "") == error.ToString())
                {
                    return ((int)(error)).ToString();
                }
            }
            return "";
        }
    }
}
