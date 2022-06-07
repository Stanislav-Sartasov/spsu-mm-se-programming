using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace WeatherClasses
{
    public class WeatherCharacterization : INotifyPropertyChanged
    {
        private string site;
        private string temperature;
        private string windSpeed;
        private string windDirection;
        private string cloudCover;
        private string humidity;
        private string precipitation;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Site
        {
            get { return site; }
            set
            {
                if (site != value)
                {
                    site = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Temperature
        {
            get { return temperature; }
            set
            {
                if (temperature != value)
                {
                    temperature = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WindSpeed
        {
            get { return windSpeed; }
            set
            {
                if (windSpeed != value)
                {
                    windSpeed = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WindDirection
        {
            get { return windDirection; }
            set
            {
                if (windDirection != value)
                {
                    windDirection = value;
                    OnPropertyChanged();
                }
            }
        }
        public string CloudCover
        {
            get { return cloudCover; }
            set
            {
                if (cloudCover != value)
                {
                    cloudCover = value;
                    OnPropertyChanged();
                }            }
        }

        public string Humidity
        {
            get { return humidity; }
            set
            {
                if (humidity != value)
                {
                    humidity = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Precipitation
        {
            get { return precipitation; }
            set
            {
                if (precipitation != value)
                {
                    precipitation = value;
                    OnPropertyChanged();
                }
            }
        }

        public void FillProperties(Dictionary<string, string> parameters)
        {
            if (parameters is null)
            {
                Site = "It's currently unavailable";
                WindSpeed = "unavailable";
                WindDirection = "unavailable";
                CloudCover = "unavailable";
                Humidity = "unavailable";
                Precipitation = "unavailable";
                Temperature = "unavailable";
                return;
            }
            Site = parameters["site"];
            WindSpeed = parameters["windSpeed"] + " in m/s";
            WindDirection = parameters["windDirection"] + " in degrees";
            CloudCover = parameters["cloudCover"] + " in %";
            Humidity = parameters["humidity"] +  " in %";
            Precipitation = parameters["precipitation"] + " in %";
            Temperature = String.Format("{0} in Celsius, {1} in Fahrenheits",
                parameters["temperature"], parameters["fahrenheitTemperature"]);
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void ChangeWeather(WeatherCharacterization newWeather)
        {
            this.Site = newWeather.Site;
            this.Temperature = newWeather.Temperature;
            this.WindDirection = newWeather.WindDirection;
            this.WindSpeed = newWeather.WindSpeed;
            this.Humidity = newWeather.Humidity;
            this.Precipitation = newWeather.Precipitation;
            this.CloudCover = newWeather.CloudCover;
        }

    }
}
