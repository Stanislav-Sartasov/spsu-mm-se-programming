namespace WeatherLibrary
{
    public class Weather : AWeather
    {
        public Weather(string sourceName, string? temperatureCelsius, string? temperatureFahrenheit, string? cloudCoverage, string? humidity,
                       string? precipitation, string? windSpeed, string? windDirection)
            : base(sourceName, temperatureCelsius, temperatureFahrenheit, cloudCoverage, humidity, precipitation, windSpeed, windDirection)
        {
            FullfillTemperature();
            ConcateUnits();
        }

        private void FullfillTemperature()
        {
            if (TemperatureCelsius != "No Data" && TemperatureFahrenheit == "No Data")
            {
                try
                {
                    float tempCelsius = float.Parse(TemperatureCelsius);

                    TemperatureFahrenheit = $"{tempCelsius * 9 / 5 + 32}";
                }
                catch { }
            }
            else if (TemperatureCelsius == "No Data" && TemperatureFahrenheit != "No Data")
            {
                try
                {
                    float tempFahr = float.Parse(TemperatureFahrenheit);

                    TemperatureCelsius = $"{(tempFahr - 32) * 5 / 9}";
                }
                catch { }
            }
        }

        private void ConcateUnits()
        {
            TemperatureCelsius += TemperatureCelsius == "No Data" ? "" : "°C";
            TemperatureFahrenheit += TemperatureFahrenheit == "No Data" ? "" : "°F";
            Humidity += Humidity == "No Data" ? "" : "%";
            WindSpeed += WindSpeed == "No Data" ? "" : "m/s";
            WindDirection += WindDirection == "No Data" ? "" : "°";
        }
    }
}
