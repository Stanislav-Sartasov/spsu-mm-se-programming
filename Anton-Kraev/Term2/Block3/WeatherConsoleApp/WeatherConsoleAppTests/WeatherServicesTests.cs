using System.Reflection;
using NUnit.Framework;
using WeatherConsoleApp.ISite;
using WeatherConsoleApp.Sites.OpenWeatherMap;
using WeatherConsoleApp.Sites.TomorrowIo;

namespace WeatherConsoleAppTests
{
    public class WeatherServicesTests
    {
        [Test]
        public void OpenWeatherMapWeatherServiceTests()
        {
            var service = new OpenWeatherMapWeatherService();

            var url = "https://api.openweathermap.org/data/2.5/weather?";
            var parameters = "lat=59.94&lon=30.24&appid=";
            var key = "e2d900011a6c02deccc3e56e7534ed32";

            typeof(OpenWeatherMapWeatherService).GetField("requestMaker", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(service, new ApiRequestMaker(url, parameters, key));

            service.ShowWeather();

            Assert.IsNotNull(typeof(OpenWeatherMapWeatherService).GetField("currentWeather", BindingFlags.Instance | BindingFlags.NonPublic));
        }

        [Test]
        public void TomorrowIoWeatherServiceTests()
        {
            var service = new TomorrowIoWeatherService();

            var url = "https://api.tomorrow.io/v4/timelines?";
            var parameters = "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            var key = "aklQAoAZ4rhfO9brbUCK5p47DarnvEVO";

            typeof(TomorrowIoWeatherService).GetField("requestMaker", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(service, new ApiRequestMaker(url, parameters, key));

            service.ShowWeather();

            Assert.IsNotNull(typeof(OpenWeatherMapWeatherService).GetField("currentWeather", BindingFlags.Instance | BindingFlags.NonPublic));
        }
    }
}
