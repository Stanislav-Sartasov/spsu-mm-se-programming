namespace WeatherPattern
{
    public class WeatherPtrn
    {
        public int CloudCover { get; private set; }
        public int Humidity { get; private set; }
        public string Precipitation { get; private set; }
        public int TemperatureCelsius { get; private set; }
        public int TemperatureFahrenheit { get => (int)Math.Round(TemperatureCelsius * 1.8 + 32); }
        public int WindDirection { get; private set; }
        public double WindSpeed { get; private set; }

        public WeatherPtrn(int cloud, int humidity, string precipitation, double temperature, int windDirection, double windSpeed)
        {
            CloudCover = cloud;
            Humidity = humidity;
            Precipitation = precipitation;
            TemperatureCelsius = (int)Math.Round(temperature);
            WindDirection = windDirection;
            WindSpeed = windSpeed;
        }
    }
}
