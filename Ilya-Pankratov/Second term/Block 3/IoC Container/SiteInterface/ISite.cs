using Forecast;
using RequestManagement;

namespace SiteInterface
{
    public interface ISite
    {
        public string Name { get; }
        public WeatherParameter Parameter { get; }
        public SiteWeatherForecast GetCityWeatherForecast();
        public SiteWeatherForecast GetCityWeatherForecast(IRequest request);
        public void ChangeParameter(WeatherParameter parameter);
    }
}