using NUnit.Framework;
using System;

namespace WeatherTools.UnitTests
{
    public class WeatherTest
    {
        [Test]
        public void CreateTest()
        {
            DateTime dateTime = DateTime.Now;
            Weather weather = new Weather(5, 5, 5, "3", 5, 3, dateTime);
            Assert.AreEqual(5, weather.TemperatureInCelsius);
            Assert.AreEqual(5, weather.CloudCover);
            Assert.AreEqual(5, weather.Humidity);
            Assert.AreEqual("3", weather.Precipitation);
            Assert.AreEqual(5, weather.WindDirection);
            Assert.AreEqual(3, weather.WindSpeed);
            Assert.AreEqual(weather.TemperatureInCelsius * 1.8 + 32, weather.TemperatureInFahrenheit);
            Assert.AreEqual(dateTime, weather.Time);
        }
    }
}