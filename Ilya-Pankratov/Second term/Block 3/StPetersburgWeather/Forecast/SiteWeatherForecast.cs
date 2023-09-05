namespace Forecast
{
    public class SiteWeatherForecast
    {
        public string Source { get; private set; }
        public string ErrorMessage { get; private set; }
        public List<CityWeatherForecast> Forecast { get; private set; }

        public SiteWeatherForecast(string source, string errorMessage)
        {
            Source = source;
            ErrorMessage = errorMessage;
            Forecast = null;
        }

        public SiteWeatherForecast(string source, string errorMessage, List<CityWeatherForecast> forecast)
        {
            Source = source;
            ErrorMessage = errorMessage;
            Forecast = forecast;
        }

    }
}
