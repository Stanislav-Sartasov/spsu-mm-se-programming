using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUI_WPF.Model
{
    public class WeatherInfo
    {
        public double TempC { get; set; }
        public double TempF { get; set; }
        public double Humidity { get; set; }
        public double CloudsPercent { get; set; }
        public string Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public string WindDirection { get; set; }
    }
}
