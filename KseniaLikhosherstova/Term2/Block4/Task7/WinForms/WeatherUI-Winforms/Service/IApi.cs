using WeatherUI_Winforms.Model;

namespace WeatherUI_WPF.Service
{
    public interface IApi
    {
        string ApiName { get; }
        WeatherInfo GetData();
    }
}