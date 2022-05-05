using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherWebAPI;
using WeatherLibrary;
using System.IO;

namespace WeatherLibraryTest
{
    [TestClass]
    public class StormGlassParserTests
    {
        private string defaultValue = "No Data";
        private string path = @"../../../JSONs/StormGlass.json";
        private string name = "StormGlass";

        [TestMethod]
        public void DeserializeToWeatherFromEmptyJSONTest()
        {
            StormGlassParser parser = new StormGlassParser();

            AWeather weather = parser.DeserializeToWeather("");

            Assert.AreEqual(name, weather.SourceName);
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
            StormGlassParser parser = new StormGlassParser();
            string JSON = File.ReadAllText(path);

            AWeather weather = parser.DeserializeToWeather(JSON);

            Assert.AreEqual(name, weather.SourceName);
            Assert.AreEqual((5.6).ToString() + "°C", weather.TemperatureCelsius);
            Assert.AreEqual((42.08).ToString() + "°F", weather.TemperatureFahrenheit);
            Assert.AreEqual("Cloudy", weather.CloudCoverage);
            Assert.AreEqual((40.8).ToString() + "%", weather.Humidity);
            Assert.AreEqual(defaultValue, weather.Precipitation);
            Assert.AreEqual((4.39).ToString() + "m/s", weather.WindSpeed);
            Assert.AreEqual((292.5).ToString() + "°", weather.WindDirection);
        }
    }
}
