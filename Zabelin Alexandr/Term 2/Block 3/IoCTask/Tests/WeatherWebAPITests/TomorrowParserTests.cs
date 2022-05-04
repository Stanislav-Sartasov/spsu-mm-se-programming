using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherWebAPI;
using WeatherLibrary;
using System.IO;

namespace WeatherLibraryTest
{
    [TestClass]
    public class TomorrowParserTests
    {
        private string defaultValue = "No Data";
        private string path = @"../../../JSONs/TomorrowIO.json";

        [TestMethod]
        public void DeserializeToWeatherFromEmptyJSONTest()
        {
            TomorrowParser parser = new TomorrowParser();

            AWeather weather = parser.DeserializeToWeather("");

            Assert.AreEqual("TomorrowIO", weather.SourceName);
            Assert.AreEqual(defaultValue, weather.TemperatureCelsius);
            Assert.AreEqual(defaultValue, weather.TemperatureFahrenheit);
            Assert.AreEqual(defaultValue, weather.CloudCoverage);
            Assert.AreEqual(defaultValue, weather.Humidity);
            Assert.AreEqual(defaultValue, weather.Precipitation);
            Assert.AreEqual(defaultValue, weather.WindSpeed);
            Assert.AreEqual(defaultValue, weather.WindDirection);
        }

        [TestMethod]
        public void DeserializeToWeatherFromFullJSONTest()
        {
            TomorrowParser parser = new TomorrowParser();
            string JSON = File.ReadAllText(path);

            AWeather weather = parser.DeserializeToWeather(JSON);

            Assert.AreEqual("TomorrowIO", weather.SourceName);
            Assert.AreEqual("5.01°C", weather.TemperatureCelsius);
            Assert.AreEqual("41.018°F", weather.TemperatureFahrenheit);
            Assert.AreEqual("Clear", weather.CloudCoverage);
            Assert.AreEqual("46.98%", weather.Humidity);
            Assert.AreEqual("Sunny", weather.Precipitation);
            Assert.AreEqual("8.27m/s", weather.WindSpeed);
            Assert.AreEqual("321.5°", weather.WindDirection);
        }
    }
}

