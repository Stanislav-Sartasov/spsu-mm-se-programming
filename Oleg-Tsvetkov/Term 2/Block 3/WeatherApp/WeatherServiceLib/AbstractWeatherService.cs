namespace WeatherServiceLib
{
    public abstract class AbstractWeatherService
    {
        protected double CurrentLatitude;
        protected double CurrentLongitude;
        protected float? LastTemperatureCelsius;
        protected float? LastTemperatureFahrenheit;
        protected float? LastCloudCover;
        protected float? LastHumidity;
        protected float? LastPrecipitation;
        protected float? LastWindDirection;
        protected float? LastWindSpeed;

        protected AbstractWeatherService(double lat, double lon)
        {
            CurrentLatitude = lat;
            CurrentLongitude = lon;
        }
        public abstract bool UpdateInfo();

        public void PrintInfo()
        {
            Console.WriteLine("Температура по Цельсию: " + (LastTemperatureCelsius is null ? "Нет данных" : LastTemperatureCelsius));
            Console.WriteLine("Температура по Фаренгейту: " + (LastTemperatureFahrenheit is null ? "Нет данных" : LastTemperatureFahrenheit));
            Console.WriteLine("Облачность в процентах: " + (LastCloudCover is null ? "Нет данных" : LastCloudCover));
            Console.WriteLine("Влажность в процентах: " + (LastHumidity is null ? "Нет данных" : LastHumidity));
            Console.WriteLine("Осадки в формате mm/h: " + (LastPrecipitation is null ? "Нет данных" : LastPrecipitation));
            Console.WriteLine("Направление ветра в градусах(0 - север): " + (LastWindDirection is null ? "Нет данных" : LastWindDirection));
            Console.WriteLine("Скорость ветра, м/с: " + (LastWindSpeed is null ? "Нет данных" : LastWindSpeed));
        }
    }
}