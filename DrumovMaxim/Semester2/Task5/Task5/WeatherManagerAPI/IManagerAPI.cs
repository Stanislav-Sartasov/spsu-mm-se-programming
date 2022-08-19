using WeatherPattern;

namespace WeatherManagerAPI
{
    public interface IManagerAPI
    {
        WeatherPtrn? WeatherData { get; }
        string Name { get; }
        string WebAddress { get; }
        string GetResponse(string url);
        WeatherPtrn GetWeather(string response);
        void EmptyPattern();
    }
}
