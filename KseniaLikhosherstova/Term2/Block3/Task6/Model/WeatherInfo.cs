namespace Model
{
    public class WeatherInfo
    {
        public double TempC { get; private set; }
        public double TempF { get; private set; }
        public double Humidity { get; private set; }
        public double CloudsPercent { get; private set; }
        public string Precipitation { get; private set; }
        public double WindSpeed { get; private set; }
        public string? WindDirection { get; private set; }


        public WeatherInfo(double tempC, double tempF, double cloud, double humidity, string precipitation, double windDirect, double windSpeed)
        {

            TempC = tempC;
            TempF = tempF;
            Humidity = humidity;
            CloudsPercent = cloud;
            Precipitation = precipitation;
            WindSpeed = windSpeed;
            WindDirection = GetWindDirection(windDirect);
        }

        public static string? GetWindDirection(double? deg)
        {
            if (deg == null) return null;
            string north = "North";
            string northEast = "Northeast";
            string east = "East";
            string southEast = "Southeast";
            string south = "South";
            string southWest = "Southwest";
            string west = "West";
            string northWest = "Northwest";

            string direct = deg switch
            {
                >= 0 and <= 22.5 => north,
                > 22.5 and <= 67.5 => northEast,
                > 67.5 and <= 112.5 => east,
                > 112.5 and <= 157.5 => southEast,
                > 157.5 and <= 202.5 => south,
                > 202.5 and <= 247.5 => southWest,
                > 247.5 and <= 292.5 => west,
                > 292.5 and <= 337.5 => northWest,
                > 337.5 and <= 360 => north,
                _ => throw new ArgumentOutOfRangeException(nameof(deg))
            };
            return direct;
        }

    }
}