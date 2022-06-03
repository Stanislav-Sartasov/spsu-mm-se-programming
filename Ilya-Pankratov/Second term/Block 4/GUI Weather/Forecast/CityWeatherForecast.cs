using System.ComponentModel;

namespace Forecast
{
    public class CityWeatherForecast : INotifyPropertyChanged
    {
        private string celsiusTemperature;
        private string fahrenheitTemperature;
        private string cloudCover;
        private string humidity;
        private string windDirection;
        private string windSpeed;

        public string CelsiusTemperature
        {
            get
            {
                return celsiusTemperature;
            }
            set
            {
                celsiusTemperature = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CelsiusTemperature"));
            }
        }

        public string FahrenheitTemperature
        {
            get
            {
                return fahrenheitTemperature;
            }
            set
            {
                fahrenheitTemperature = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FahrenheitTemperature"));
            }
        }

        public string CloudCover
        {
            get
            {
                return cloudCover;
            }
            set
            {
                cloudCover = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CloudCover"));
            }
        }

        public string Humidity
        {
            get
            {
                return humidity;
            }
            set
            {
                humidity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Humidity"));
            }
        }

        public string WindDirection
        {
            get
            {
                return windDirection;
            }
            set
            {
                windDirection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WindDirection"));
            }
        }

        public string WindSpeed
        {
            get
            {
                return windSpeed;
            }
            set
            {
                windSpeed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WindSpeed"));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public CityWeatherForecast(string celsiusTemperature, string cloudCover, string humidity, string windDir, string windSpeed)
        {
            this.celsiusTemperature = celsiusTemperature;
            fahrenheitTemperature = celsiusTemperature == "Not Stated" ? celsiusTemperature : Math.Round(Convert.ToDouble(CelsiusTemperature) * 1.8 + 32, 2).ToString();
            this.cloudCover = cloudCover;
            this.humidity = humidity;
            this.windDirection = windDir;
            this.windSpeed = windSpeed;
        }
    }
}