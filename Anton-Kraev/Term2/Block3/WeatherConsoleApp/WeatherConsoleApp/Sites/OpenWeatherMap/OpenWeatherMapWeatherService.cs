using WeatherConsoleApp.ISite;
using WeatherConsoleApp.WeatherInfo;

namespace WeatherConsoleApp.Sites.OpenWeatherMap
{
    public class OpenWeatherMapWeatherService : IWeatherService
    {
        private ApiRequestMaker requestMaker;
        private Weather? currentWeather;

        public OpenWeatherMapWeatherService()
        {
            var url = "https://api.openweathermap.org/data/2.5/weather?";
            var parameters = "lat=59.94&lon=30.24&appid=";
            var key = Environment.GetEnvironmentVariable("OpenWeatherMapApiKey");
            requestMaker = new ApiRequestMaker(url, parameters, key);
        }

        public void ShowWeather()
        {
            currentWeather = OpenWeatherMapJsonParser.Parse(requestMaker.GetResponse());

            if (currentWeather != null)
            {
                Console.WriteLine("Data from OpenWeatherMap:");
                ConsoleWriter.ShowWeather(currentWeather);
            }
            else
            {
                Console.WriteLine("Unable to get data from the OpenWeatherMap:");
                Console.WriteLine(requestMaker.AccessError);
            }
        }
    }
}