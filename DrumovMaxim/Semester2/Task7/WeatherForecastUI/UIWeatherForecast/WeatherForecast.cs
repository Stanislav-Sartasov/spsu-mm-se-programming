using System.ComponentModel;
using WeatherPattern;

namespace UIWeatherForecast
{
    public class CityWeatherForecast : INotifyPropertyChanged
    {
        private WeatherPtrn? forecastData;
        private string? exceptionMessage;
        public event PropertyChangedEventHandler? PropertyChanged;

        public WeatherPtrn? ForecastData
        {
            get
            {
                return forecastData;
            }
            set
            {
                forecastData = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeatherData"));
            }
        }

        public string? ExceptionMessage
        {
            get
            {
                return exceptionMessage;
            }
            set
            {
                exceptionMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeatherData"));
            }
        }

        private void EmptySet()
        {
            forecastData = null;
            exceptionMessage = null;
        }

        public void UpdateWeather(Type type)
        {

        }

        private void GetUpdateWeather(Type type)
        {
            EmptySet();


        }
    }
}