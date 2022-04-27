namespace WeatherConsoleApp.WeatherInfo
{
    public class Weather
    {
        private double TempInKelvin { get; }
        public int Humidity { get; }
        public int Cloudiness { get; }
        public string Precipitation { get; }
        public double WindSpeed { get; }
        private int WindDegree { get; }
        public double TempInCelcius => Math.Round(TempInKelvin - 273.15, 2);
        public double TempInFahrenheit => Math.Round((TempInKelvin - 273.15) * 1.8 + 32, 2);
        public string WindDir => ((WindDirection)(WindDegree / 45 % 8)).ToString();

        public Weather(double tempInKelvin, int humidity, int cloudiness, string precipitation, int windDegree, double windSpeed)
        {
            TempInKelvin = tempInKelvin;
            Humidity = humidity;
            Cloudiness = cloudiness;
            Precipitation = precipitation;
            WindDegree = windDegree; 
            WindSpeed = windSpeed;
        }
    }
}