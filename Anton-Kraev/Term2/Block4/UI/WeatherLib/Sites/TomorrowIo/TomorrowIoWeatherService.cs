using WeatherLib.ISite;
using WeatherLib.WeatherInfo;

namespace WeatherLib.Sites.TomorrowIo
{
    public class TomorrowIoWeatherService : IWeatherService
    {
        private ApiRequestMaker requestMaker;
        public Weather? CurrentWeather { get; set; }

        public TomorrowIoWeatherService()
        {
            var url = "https://api.tomorrow.io/v4/timelines?";
            var parameters = "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            var key = Environment.GetEnvironmentVariable("TomorrowIoApiKey");
            requestMaker = new ApiRequestMaker(url, parameters, key);
        }

        public void UpdateWeather()
        {
            TomorrowIoParser.Parse(CurrentWeather!, requestMaker.GetResponse());
        }
    }
}