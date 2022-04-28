using System.Reflection;
using NUnit.Framework;
using WeatherConsoleApp.ISite;

namespace WeatherConsoleAppTests
{
    public class RequestMakerTests
    {
        [Test]
        public void OpenWeatherMapRequestsTests()
        {
            var url = "https://api.openweathermap.org/data/2.5/weather?";
            var parameters = "lat=59.94&lon=30.24&appid=";
            var key = "e2d900011a6c02deccc3e56e7534ed32";
            ApiRequestMaker apiRequestMaker = new ApiRequestMaker(url, parameters, key);
            
            Assert.IsNotNull(apiRequestMaker.GetResponse()); 
            Assert.IsNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(apiRequestMaker, "dgo432fdgm75452idfomm");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(apiRequestMaker, "e2d900011a6c02deccc3e56e7534ed32");
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("parameters", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(apiRequestMaker, "fdsou4324fh43252nsmdp");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("parameters", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(apiRequestMaker, parameters);
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.AccessError);
        }

        [Test]
        public void TomorrowIoRequestsTests()
        {
            var url = "https://api.tomorrow.io/v4/timelines?";
            var parameters = "timesteps=current&location=59.94,30.24&fields=temperature,humidity,windSpeed,windDirection,precipitationType,cloudCover&units=metric&apikey=";
            var key = "aklQAoAZ4rhfO9brbUCK5p47DarnvEVO";
            var apiRequestMaker = new ApiRequestMaker(url, parameters, key);

            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(apiRequestMaker, "dgo432fdgm75452idfomm");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(apiRequestMaker, "aklQAoAZ4rhfO9brbUCK5p47DarnvEVO");
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("parameters", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(apiRequestMaker, "fdsou4324fh43252nsmdp");
            Assert.IsNull(apiRequestMaker.GetResponse());
            Assert.IsNotNull(apiRequestMaker.AccessError);

            typeof(ApiRequestMaker).GetField("parameters", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.SetValue(apiRequestMaker, parameters);
            Assert.IsNotNull(apiRequestMaker.GetResponse());
            Assert.IsNull(apiRequestMaker.AccessError);
        }
    }
}