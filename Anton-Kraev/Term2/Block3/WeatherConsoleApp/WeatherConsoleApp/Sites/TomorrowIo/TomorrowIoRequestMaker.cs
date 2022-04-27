using WeatherConsoleApp.ApiRequestMaker;

namespace WeatherConsoleApp.Sites.TomorrowIo
{
    public class TomorrowIoRequestMaker : AbstractApiRequestMaker
    {
        public TomorrowIoRequestMaker()
        {
            url = "https://api.tomorrow.io/v4/timelines?";
            parameters =
                "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            key = Environment.GetEnvironmentVariable("TomorrowIoApiKey");
        }
    }
}