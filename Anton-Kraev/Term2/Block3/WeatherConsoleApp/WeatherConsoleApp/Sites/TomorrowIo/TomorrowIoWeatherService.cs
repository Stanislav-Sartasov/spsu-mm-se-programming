using WeatherConsoleApp.ISite;
using WeatherConsoleApp.WeatherInfo;

namespace WeatherConsoleApp.Sites.TomorrowIo
{
    public class TomorrowIoWeatherService : IWeatherService
    {
        private ApiRequestMaker requestMaker;
        private Weather? currentWeather;

        public TomorrowIoWeatherService()
        {
            var url = "https://api.tomorrow.io/v4/timelines?";
            var parameters = "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            var key = Environment.GetEnvironmentVariable("TomorrowIoApiKey");
            requestMaker = new ApiRequestMaker(url, parameters, key);
        }

        public void ShowWeather()
        {
            currentWeather = TomorrowIoJsonParser.Parse(requestMaker.GetResponse());

            if (currentWeather != null)
            {
                Console.WriteLine("Data from TomorrowIo:");
                ConsoleWriter.ShowWeather(currentWeather);
            }
            else
            {
                Console.WriteLine("Unable to get data from the TomorrowIo:");
                Console.WriteLine(requestMaker.AccessError);
            }
        }
    }
}