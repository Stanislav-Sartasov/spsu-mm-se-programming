namespace WeatherPattern
{
    public class WeatherPtrn
    {
        public int CloudCover { get; private set; }
        public int Humidity { get; private set; }
        public string Precipitation { get; private set; }
        public double Temperature { get; private set; }
        public int WindDirection { get; private set; }
        public double WindSpeed { get; private set; }

        public WeatherPtrn(int cloud, int humidity, string precipitation, double temperature, int windDirection, double windSpeed)
        {
            CloudCover = cloud;
            Humidity = humidity;
            Precipitation = precipitation;
            Temperature = temperature;
            WindDirection = windDirection;
            WindSpeed = windSpeed;
        }
    }
}
