using Moq;
using NUnit.Framework;
using RequestManagement;
using SiteInterface;
using System.Linq;

namespace Sites.UnitTests
{
    public class StormGlassTests
    {
        [Test]
        public void ConstructorTest()
        {
            // first case

            var firstSite = new StormGlass(WeatherParameter.Current);

            Assert.IsNotNull(firstSite);
            Assert.AreEqual(firstSite.Name, "StormGlass");
            Assert.AreEqual(firstSite.Parameter, WeatherParameter.Current);

            // second case

            var secondSite = new StormGlass(WeatherParameter.Week);

            Assert.IsNotNull(secondSite);
            Assert.AreEqual(secondSite.Name, "StormGlass");
            Assert.AreEqual(secondSite.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void ChangeParameterTest()
        {
            var site = new StormGlass(WeatherParameter.Current);
            site.ChangeParameter(WeatherParameter.Week);

            Assert.AreEqual(site.Parameter, WeatherParameter.Week);
        }

        [Test]
        public void GetCityWeatherForecastDayTest()
        {
            var request = new Mock<IRequest>();
            var jsonString = "{\"hours\":[{\"airTemperature\":{\"noaa\":3.55},\"cloudCover\":{\"noaa\":88.7},\"humidity\":" +
                             "{\"noaa\":75.4},\"windDirection\":{\"noaa\":303.32},\"windSpeed\":{\"noaa\":3.65}}]}";
            request.Setup(x => x.Get()).Returns(jsonString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new StormGlass();
            var result = site.GetCityWeatherForecast(request.Object);
            var todayWeather = result.Forecast[0];


            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "StormGlass");
            Assert.IsTrue(result.ErrorMessage == "No errors");
            Assert.IsTrue(todayWeather.CelsiusTemperature == "3,55");
            Assert.IsTrue(todayWeather.FahrenheitTemperature == "38,39");
            Assert.IsTrue(todayWeather.Humidity == "75,4");
            Assert.IsTrue(todayWeather.CloudCover == "88,7");
            Assert.IsTrue(todayWeather.WindSpeed == "3,65");
            Assert.IsTrue(todayWeather.WindDirection == "303,32");
        }

        [Test]
        public void GetCityWeatherForecastWeekTest()
        {
            var request = new Mock<IRequest>();
            var jsonDay = "{\"airTemperature\":{\"noaa\":3.55},\"cloudCover\":{\"noaa\":88.7},\"humidity\":" +
                          "{\"noaa\":75.4},\"windDirection\":{\"noaa\":303.32},\"windSpeed\":{\"noaa\":3.65}},";
            var temp = string.Concat(Enumerable.Repeat(jsonDay, 145));
            var temp2 = temp.Remove(temp.LastIndexOf(','), 1);
            var jsonWeekString = "{\"hours\":[" + temp2 + "]}";
            request.Setup(x => x.Get()).Returns(jsonWeekString);
            request.Setup(x => x.Connected).Returns(true);

            var site = new StormGlass(WeatherParameter.Week);
            var result = site.GetCityWeatherForecast(request.Object);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Source == "StormGlass");
            Assert.IsTrue(result.ErrorMessage == "No errors");

            foreach (var todayWeather in result.Forecast)
            {
                Assert.IsTrue(todayWeather.CelsiusTemperature == "3,55");
                Assert.IsTrue(todayWeather.FahrenheitTemperature == "38,39");
                Assert.IsTrue(todayWeather.Humidity == "75,4");
                Assert.IsTrue(todayWeather.CloudCover == "88,7");
                Assert.IsTrue(todayWeather.WindSpeed == "3,65");
                Assert.IsTrue(todayWeather.WindDirection == "303,32");
            }
        }
    }
}