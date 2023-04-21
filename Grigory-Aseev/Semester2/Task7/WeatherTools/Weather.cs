namespace WeatherTools
{
    public class Weather
    {
        public int TemperatureInCelsius { get; private set; }

        public int TemperatureInFahrenheit
        {
            get => (int)Math.Round(1.8 * TemperatureInCelsius + 32);
        }

        public int CloudCover { get; private set; }
        public int Humidity { get; private set; }
        public string Precipitation { get; private set; }
        public int WindDirection { get; private set; }
        public double WindSpeed { get; private set; }
        public DateTime Time { get; private set; }

        public Weather(double temp, int cloud, int humidity, string precipitation, int windDirect, double windSpeed, DateTime time)
        {
            TemperatureInCelsius = (int)Math.Round(temp);
            CloudCover = cloud;
            Humidity = humidity;
            Precipitation = precipitation;
            WindDirection = windDirect;
            WindSpeed = windSpeed;
            Time = time;
        }
    }
}