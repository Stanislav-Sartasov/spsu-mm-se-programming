using WeatherUI_WPF.Model;

namespace WeatherUI_WPF.Service
{
    public interface IApi
    {
        string ApiName { get; }
        WeatherInfo GetData();
    }
}