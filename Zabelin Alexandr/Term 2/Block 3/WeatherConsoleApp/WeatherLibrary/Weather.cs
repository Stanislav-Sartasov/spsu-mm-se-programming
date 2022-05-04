namespace WeatherLibrary
{
    public class Weather
    {
        public string SourceName { get; private set; }
        public string? TemperatureCelsius { get; private set; }
        public string? TemperatureFahrenheit { get; private set; }
        public string? CloudCoverage { get; private set; }
        public string? Humidity { get; private set; }
        public string? Precipitation { get; private set; }
        public string? WindSpeed { get; private set; }
        public string? WindDirection { get; private set; }


        public Weather(string sourceName, string? temperatureCelsius, string? temperatureFahrenheit, string? cloudCoverage, string? humidity,
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

            FullfillTemperature();
            ConcateUnits();
        }

        public void PrintAll()
        {
            Console.WriteLine("----------------------------");

            Console.WriteLine($"Source: {SourceName}:\n");
            Console.WriteLine($"Temperature: {TemperatureCelsius}, {TemperatureFahrenheit}");
            Console.WriteLine($"Cloud cover: {CloudCoverage}");
            Console.WriteLine($"Humidity: {Humidity}");
            Console.WriteLine($"Precipitation: {Precipitation}");
            Console.WriteLine($"WindSpeed: {WindSpeed}");
            Console.WriteLine($"Wind direction from north: {WindDirection}");

            Console.WriteLine("----------------------------");
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