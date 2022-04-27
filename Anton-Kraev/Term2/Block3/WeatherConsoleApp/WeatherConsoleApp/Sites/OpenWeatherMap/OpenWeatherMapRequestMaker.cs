using WeatherConsoleApp.ApiRequestMaker;

namespace WeatherConsoleApp.Sites.OpenWeatherMap
{
    public class OpenWeatherMapRequestMaker : AbstractApiRequestMaker
    {
        public OpenWeatherMapRequestMaker()
        {
            url = "https://api.openweathermap.org/data/2.5/weather?";
            parameters = "lat=59.94&lon=30.24&appid=";
            key = Environment.GetEnvironmentVariable("OpenWeatherMapApiKey");
        }
    }
}