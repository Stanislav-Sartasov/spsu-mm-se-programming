using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteInterface;

namespace SaintPetersburgWeatherWinForms.Output
{
    public class InfoDisplayer : INotifyPropertyChanged
    {
        private string sourceName = string.Empty;
        private WeatherParameter parameter;
        public string SourceName 
        {
            get
            {
                return sourceName;
            }
            set
            {
                sourceName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SourceName"));
            }
        }

        public WeatherParameter Parameter
        {
            get
            {
                return parameter;
            }
            set
            {
                parameter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Parameter"));
            }
        }

        public InfoDisplayer(string sourceName, WeatherParameter parameter)
        {
            this.sourceName = sourceName;
            this.parameter = parameter;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}