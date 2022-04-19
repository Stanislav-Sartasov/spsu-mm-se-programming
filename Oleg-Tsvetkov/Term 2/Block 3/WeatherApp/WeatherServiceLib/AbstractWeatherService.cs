namespace WeatherServiceLib
{
    public abstract class AbstractWeatherService
    {
        protected double currentLatitude;
        protected double currentLongitude;
        protected float? lastTemperatureCelsius;
        protected float? lastTemperatureFahrenheit;
        protected float? lastCloudCover;
        protected float? lastHumidity;
        protected float? lastPrecipitation;
        protected float? lastWindDirection;
        protected float? lastWindSpeed;

        protected AbstractWeatherService(double lat, double lon)
        {
            currentLatitude = lat;
            currentLongitude = lon;
        }
        public abstract bool UpdateInfo();

        public void PrintInfo()
        {
            Console.WriteLine("Температура по Цельсию: " + (lastTemperatureCelsius is null ? "Нет данных" : lastTemperatureCelsius));
            Console.WriteLine("Температура по Фаренгейту: " + (lastTemperatureFahrenheit is null ? "Нет данных" : lastTemperatureFahrenheit));
            Console.WriteLine("Облачность в процентах: " + (lastCloudCover is null ? "Нет данных" : lastCloudCover));
            Console.WriteLine("Влажность в процентах: " + (lastHumidity is null ? "Нет данных" : lastHumidity));
            Console.WriteLine("Осадки в формате mm/h: " + (lastPrecipitation is null ? "Нет данных" : lastPrecipitation));
            Console.WriteLine("Направление ветра в градусах(0 - север): " + (lastWindDirection is null ? "Нет данных" : lastWindDirection));
            Console.WriteLine("Скорость ветра, м/с: " + (lastWindSpeed is null ? "Нет данных" : lastWindSpeed));
        }
    }
}