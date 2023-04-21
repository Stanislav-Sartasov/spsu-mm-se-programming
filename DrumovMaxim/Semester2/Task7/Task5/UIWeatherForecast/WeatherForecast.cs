using System.ComponentModel;
using IoCContainer;
using WeatherManagerAPI;
using WeatherPattern;

namespace UIWeatherForecast
{
    public class WeatherForecast : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private WeatherPtrn? forecastData;
        private string? exceptionMessage;

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ExceptionMessage"));
            }
        }

        private void EmptySet()
        {
            ForecastData = null;
            ExceptionMessage = null;
        }

        public void UpdateWeather(Type type)
        {
            GetUpdateWeather(type);
        }

        private void GetUpdateWeather(Type type)
        {
            List<bool> siteList = new List<bool>() { false, false };
            EmptySet();

            if (type.Equals(typeof(TomorrowIO)))
            {
                siteList[0] = true;
            }
            else
            {
                siteList[1] = true;
            }

            var container = new WeatherIoCContainer();
            var APIs = container.GetTypesContainer(siteList);

            foreach (var api in APIs)
            {
                api.GetWeather(api.GetResponse(api.WebAddress));

                if (api.WeatherData != null)
                {
                    ForecastData = api.WeatherData;
                }
                if (!api.State)
                {
                    ExceptionMessage = $"Exception Message: {api.ExceptionMessage}";
                }
            }


        }
    }
}