using Moq;
using NUnit.Framework;
using RequestManagement;
using SiteInterface;
using System.Linq;

namespace Sites.UnitTests
{
    public class OpenWeatherTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case

            var firstSite = new OpenWeather(WeatherParameter.Current);

            Assert.IsNotNull(firstSite);
            Assert.AreEqual(firstSite.Name, "OpenWeather");
            Assert.AreEqual(firstSite.Parameter, WeatherParameter.Current);

            // second case

            var secondSite = new OpenWeather(WeatherParameter.Week);

            Assert.IsNotNull(secondSite);
            Assert.AreEqual(secondSite.Name, "OpenWeather");
            Assert.AreEqual(secondSite.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void ChangeParameterTest()
        {
            var site = new OpenWeather(WeatherParameter.Current);
            site.ChangeParameter(WeatherParameter.Week);

            Assert.AreEqual(site.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void GetCityWeatherForecastDayTest()
        {
            var request = new Mock<IRequest>();
            var jsonString = "{\"current\":{\"temp\": 4.2,\"humidity\":99,\"clouds\":98,\"wind_speed\":10,\"wind_deg\":55}}";
            request.Setup(x => x.Get()).Returns(jsonString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new OpenWeather();
            var result = site.GetCityWeatherForecast(request.Object);
            var todayWeather = result.Forecast[0];


            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "OpenWeather");
            Assert.IsTrue(result.ErrorMessage == "No errors");
            Assert.IsTrue(todayWeather.CelsiusTemperature == "4,2");
            Assert.IsTrue(todayWeather.FahrenheitTemperature == "39,56");
            Assert.IsTrue(todayWeather.Humidity == "99");
            Assert.IsTrue(todayWeather.CloudCover == "98");
            Assert.IsTrue(todayWeather.WindSpeed == "10");
            Assert.IsTrue(todayWeather.WindDirection == "55");
        }

        [Test]
        public void GetCityWeatherForecastWeekTest()
        {
            var request = new Mock<IRequest>();
            var jsonDay = "{\"temp\": {\"day\":8.4},\"humidity\":70,\"wind_speed\":6.21,\"wind_deg\":290,\"clouds\":50},";
            var temp = string.Concat(Enumerable.Repeat(jsonDay, 7));
            temp = temp.Remove(temp.LastIndexOf(','), 1);
            var jsonWeekString = "{\"daily\":[" + temp + "]}";
            request.Setup(x => x.Get()).Returns(jsonWeekString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new OpenWeather(WeatherParameter.Week);
            var result = site.GetCityWeatherForecast(request.Object);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "OpenWeather");
            Assert.IsTrue(result.ErrorMessage == "No errors");

            foreach (var todayWeather in result.Forecast)
            {
                Assert.IsTrue(todayWeather.CelsiusTemperature == "8,4");
                Assert.IsTrue(todayWeather.FahrenheitTemperature == "47,12");
                Assert.IsTrue(todayWeather.Humidity == "70");
                Assert.IsTrue(todayWeather.CloudCover == "50");
                Assert.IsTrue(todayWeather.WindSpeed == "6,21");
                Assert.IsTrue(todayWeather.WindDirection == "290");
            }
        }
    }
}