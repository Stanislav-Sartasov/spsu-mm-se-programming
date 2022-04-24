using WeatherConsoleApp.ApiRequestMaker;

namespace WeatherConsoleApp.Sites.TomorrowIo
{
    public class ApiRequestMaker : AbstractApiRequestMaker
    {
        public ApiRequestMaker()
        {
            url = "https://api.tomorrow.io/v4/timelines?";
            parameters =
                "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            key = "aklQAoAZ4rhfO9brbUCK5p47DarnvEVO";
        }

        public override void SetDefaultApiKey()
        {
            key = "aklQAoAZ4rhfO9brbUCK5p47DarnvEVO";
        }
    }
}