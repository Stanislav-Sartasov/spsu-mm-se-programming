using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLibrary
{
    public abstract class AWeather
    {
        public string SourceName { get; protected set; }
        public string? TemperatureCelsius { get; protected set; }
        public string? TemperatureFahrenheit { get; protected set; }
        public string? CloudCoverage { get; protected set; }
        public string? Humidity { get; protected set; }
        public string? Precipitation { get; protected set; }
        public string? WindSpeed { get; protected set; }
        public string? WindDirection { get; protected set; }

        public AWeather(string sourceName, string? temperatureCelsius, string? temperatureFahrenheit, string? cloudCoverage, string? humidity,
                       string? precipitation, string? windSpeed, string? windDirection)
        {
            SourceName = sourceName;
            TemperatureCelsius = temperatureCelsius;
            TemperatureFahrenheit = temperatureFahrenheit;
            CloudCoverage = cloudCoverage;
            Humidity = humidity;
            Precipitation = precipitation;
            WindSpeed = windSpeed;
            WindDirection = windDirection;
        }
    }
}
