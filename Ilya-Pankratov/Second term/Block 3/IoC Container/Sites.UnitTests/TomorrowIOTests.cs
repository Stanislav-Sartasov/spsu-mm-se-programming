using Moq;
using NUnit.Framework;
using RequestManagement;
using SiteInterface;
using System.Linq;

namespace Sites.UnitTests
{
    public class TomorrowIOTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case

            var firstSite = new TomorrowIO(WeatherParameter.Current);

            Assert.IsNotNull(firstSite);
            Assert.AreEqual(firstSite.Name, "TomorrowIO");
            Assert.AreEqual(firstSite.Parameter, WeatherParameter.Current);

            // second case

            var secondSite = new TomorrowIO(WeatherParameter.Week);

            Assert.IsNotNull(secondSite);
            Assert.AreEqual(secondSite.Name, "TomorrowIO");
            Assert.AreEqual(secondSite.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void ChangeParameterTest()
        {
            var site = new TomorrowIO(WeatherParameter.Current);
            site.ChangeParameter(WeatherParameter.Week);

            Assert.AreEqual(site.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void GetCityWeatherForecastDayTest()
        {
            var request = new Mock<IRequest>();
            var jsonString = "{\"data\":{\"timelines\":[{\"intervals\":[{\"values\":{\"cloudCover\":51,\"temperature\":9,\"windDirection\":307.81,\"windSpeed\":4.19}}]}]}}";
            request.Setup(x => x.Get()).Returns(jsonString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new TomorrowIO();
            var result = site.GetCityWeatherForecast(request.Object);
            var todayWeather = result.Forecast[0];


            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "TomorrowIO");
            Assert.IsTrue(result.ErrorMessage == "No errors");
            Assert.IsTrue(todayWeather.CelsiusTemperature == "9");
            Assert.IsTrue(todayWeather.FahrenheitTemperature == "48,2");
            Assert.IsTrue(todayWeather.Humidity == "No data");
            Assert.IsTrue(todayWeather.CloudCover == "51");
            Assert.IsTrue(todayWeather.WindSpeed == "4,19");
            Assert.IsTrue(todayWeather.WindDirection == "307,81");
        }

        [Test]
        public void GetCityWeatherForecastWeekTest()
        {
            var request = new Mock<IRequest>();
            var jsonDay = "{\"values\":{\"cloudCover\":79.34,\"temperature\":9.63,\"windDirection\":302.17,\"windSpeed\":5.38}},";
            var temp = string.Concat(Enumerable.Repeat(jsonDay, 7));
            var temp2 = temp.Remove(temp.LastIndexOf(','), 1);
            var jsonWeekString = "{\"data\":{\"timelines\":[{\"intervals\":[" + temp2 + "]}]}}";
            request.Setup(x => x.Get()).Returns(jsonWeekString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new TomorrowIO(WeatherParameter.Week);
            var result = site.GetCityWeatherForecast(request.Object);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "TomorrowIO");
            Assert.IsTrue(result.ErrorMessage == "No errors");

            foreach (var todayWeather in result.Forecast)
            {
                Assert.IsTrue(todayWeather.CelsiusTemperature == "9,63");
                Assert.IsTrue(todayWeather.FahrenheitTemperature == "49,33");
                Assert.IsTrue(todayWeather.Humidity == "No data");
                Assert.IsTrue(todayWeather.CloudCover == "79,34");
                Assert.IsTrue(todayWeather.WindSpeed == "5,38");
                Assert.IsTrue(todayWeather.WindDirection == "302,17");
            }
        }
    }
}