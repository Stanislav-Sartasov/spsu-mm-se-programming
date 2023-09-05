using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherLib.WeatherInfo
{
    public class Weather : INotifyPropertyChanged
    {
        private double? tempInKelvin;
        private int? humidity;
        private int? cloudiness;
        private string? precipitation;
        private int? windDegree;
        private double? windSpeed;

        public double? TempInKelvin
        {
            get => tempInKelvin;
            set
            {
                tempInKelvin = value;
                OnPropertyChanged("TempInCelsius");
                OnPropertyChanged("TempInFahrenheit");
            }
        }
        public int? Humidity
        {
            get => humidity;
            set
            {
                humidity = value;
                OnPropertyChanged("Humidity");
            }
        }
        public int? Cloudiness
        {
            get => cloudiness;
            set
            {
                cloudiness = value;
                OnPropertyChanged("Cloudiness");
            }
        }
        public string? Precipitation
        {
            get => precipitation;
            set
            {
                precipitation = value;
                OnPropertyChanged("Precipitation");
            }
        }
        public double? WindSpeed
        {
            get => windSpeed;
            set
            {
                windSpeed = value;
                OnPropertyChanged("WindSpeed");
            }
        }
        public int? WindDegree
        {
            get => windDegree;
            set
            {
                windDegree = value;
                OnPropertyChanged("WindDirection");
            }
        }

        public double? TempInCelsius => TempInKelvin is not null ? Math.Round((double)(TempInKelvin - 273.15), 2) : null;
        public double? TempInFahrenheit => TempInKelvin is not null ? Math.Round((double)((TempInKelvin - 273.15) * 1.8 + 32), 2) : null;
        public string? WindDirection => WindDegree is not null ? ((WindDirection)(WindDegree / 45 % 8)).ToString() : null;


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}