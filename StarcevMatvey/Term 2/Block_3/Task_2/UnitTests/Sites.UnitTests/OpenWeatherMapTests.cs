using NUnit.Framework;
using Requests;
using Moq;

namespace Sites.UnitTests
{
    public class OpenWeatherMapTests
    {
        [Test]
        public void GetWeatherTest()
        {
            var getRequest = new Mock<IGetRequest>();
            getRequest.Setup(x => x.Run()).Callback(() => { });
            getRequest.Setup(x => x.Connect).Returns(false);
            OpenWeatherMap site = new OpenWeatherMap(getRequest.Object);
            Assert.Null(site.GetWeather());
            getRequest.Setup(x => x.Response).Returns("temp.:11.11clouds.:11humidity.:11wind_speed.:11wind_deg.:11description.:.aa:,.icon");
            getRequest.Setup(x => x.Connect).Returns(true);
            site = new OpenWeatherMap(getRequest.Object);
            Weather.Weather weather = site.GetWeather();
            Assert.AreEqual(weather.TempC, "11,11°C");
            Assert.AreEqual(weather.Clouds, "11%");
            Assert.AreEqual(weather.WindSpeed, "11 m/s");
            Assert.AreEqual(weather.Humidity, "11%");
            Assert.AreEqual(weather.FallOut, "aa");

            Assert.Pass();
        }
    }
}