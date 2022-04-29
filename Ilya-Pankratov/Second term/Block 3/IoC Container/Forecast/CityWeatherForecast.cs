namespace Forecast
{
    public class CityWeatherForecast
    {
        public string CelsiusTemperature { get; private set; }
        public string FahrenheitTemperature { get; private set; }
        public string CloudCover { get; private set; }
        public string Humidity { get; private set; }
        public string WindDirection { get; private set; }
        public string WindSpeed { get; private set; }

        public CityWeatherForecast(string celsiusTemp, string cloudCover, string humidity, string windDir, string windSpeed)
        {
            CelsiusTemperature = celsiusTemp;
            FahrenheitTemperature = Math.Round(Convert.ToDouble(CelsiusTemperature) * 1.8 + 32, 2).ToString();
            CloudCover = cloudCover;
            Humidity = humidity;
            WindDirection = windDir;
            WindSpeed = windSpeed;
        }
    }
}