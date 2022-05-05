using Moq;
using NUnit.Framework;
using Requests;
using Sites;

namespace Sites.UnitTests
{
    public class TomorrowIoTests
    {
        [Test]
        public void GetWeatherTest()
        {
            var getRequest = new Mock<IGetRequest>();
            getRequest.Setup(x => x.Run()).Callback(() => { });
            getRequest.Setup(x => x.Connect).Returns(false);
            TomorrowIo site =  new TomorrowIo(getRequest.Object);
            Assert.Null(site.GetWeather());
            getRequest.Setup(x => x.Response).Returns("temperature.:11.11class=.y00NXe.><div class=.TTgbLt.>11%div class=.TTgbLt.>11%11 km/hdescription.:.aa:,.icon");
            getRequest.Setup(x => x.Connect).Returns(true);
            site = new TomorrowIo(getRequest.Object);
            Weather.Weather weather = site.GetWeather();
            Assert.AreEqual(weather.TempC, "11,11°C");
            Assert.AreEqual(weather.Clouds, "11%");
            Assert.AreEqual(weather.WindSpeed, "11 km/h");
            Assert.AreEqual(weather.Humidity, "11%");
            Assert.AreEqual(weather.FallOut, "No data");

            Assert.Pass();
        }
    }
}
