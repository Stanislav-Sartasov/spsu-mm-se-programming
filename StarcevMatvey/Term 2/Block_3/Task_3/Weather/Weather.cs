namespace Weather
{
    public class Weather
    {
        public string TempC { get; private set; }
        public string TempF { get; private set; }
        public string Clouds { get; private set; }
        public string Humidity { get; private set; }
        public string WindSpeed { get; private set; }
        public string WindDegree { get; private set; }
        public string FallOut { get; private set; }

        public Weather(string tempC, string tempF, string clouds, string humidity, string windSpeed, string windDegree, string fallOut)
        {
            TempC = tempC;
            TempF = tempF;
            Clouds = clouds;
            Humidity = humidity;
            WindSpeed = windSpeed;
            WindDegree = windDegree;
            FallOut = fallOut;
        }
    }
}