using WeatherLib.WeatherInfo;

namespace WeatherLib.ISite
{
    public interface IWeatherService
    {
        public Weather? CurrentWeather { get; set; }
        public void UpdateWeather();
    }
}