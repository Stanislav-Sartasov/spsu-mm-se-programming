using WeatherConsoleApp.ApiRequestMaker;

namespace WeatherConsoleApp.Sites.OpenWeatherMap
{
    public class ApiRequestMaker : AbstractApiRequestMaker
    {
        public ApiRequestMaker()
        {
            url = "https://api.openweathermap.org/data/2.5/weather?";
            parameters = "lat=59.94&lon=30.24&appid=";
            key = "e2d900011a6c02deccc3e56e7534ed32";
        }

        public override void SetDefaultApiKey()
        {
            key = "e2d900011a6c02deccc3e56e7534ed32";
        }
    }
}