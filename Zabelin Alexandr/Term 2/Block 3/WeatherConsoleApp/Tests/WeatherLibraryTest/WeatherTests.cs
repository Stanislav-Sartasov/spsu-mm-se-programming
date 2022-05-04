using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherLibrary;

namespace WeatherLibraryTest
{
    [TestClass]
    public class WeatherTests
    {
        [TestMethod]
        public void ConstructorWithoutFahrenheitTempTest()
        {
            string name = "WeatherTests";
            string temperatureCelcius = "-12";
            string temperatureFahrenheit = "No Data";
            string cloudCover = "27";
            string humidity = "45";
            string precipitation = "Sunny";
            string windSpeed = "1.2";
            string windDirection = "170";

            Weather weather = new Weather(name, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);

            Assert.AreEqual("WeatherTests", weather.SourceName);
            Assert.AreEqual("-12°C", weather.TemperatureCelsius);
            Assert.AreEqual("10.4°F", weather.TemperatureFahrenheit);
            Assert.AreEqual("27", weather.CloudCoverage);
            Assert.AreEqual("45%", weather.Humidity);
            Assert.AreEqual("Sunny", weather.Precipitation);
            Assert.AreEqual("1.2m/s", weather.WindSpeed);
            Assert.AreEqual("170°", weather.WindDirection);
        }

        [TestMethod]
        public void ConstructorWithoutCelsiusTempTest()
        {
            string name = "WeatherTests";
            string temperatureCelcius = "No Data";
            string temperatureFahrenheit = "10.4";
            string cloudCover = "27";
            string humidity = "45";
            string precipitation = "Sunny";
            string windSpeed = "1.2";
            string windDirection = "170";

            Weather weather = new Weather(name, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);

            Assert.AreEqual("WeatherTests", weather.SourceName);
            Assert.AreEqual("-12°C", weather.TemperatureCelsius);
            Assert.AreEqual("10.4°F", weather.TemperatureFahrenheit);
            Assert.AreEqual("27", weather.CloudCoverage);
            Assert.AreEqual("45%", weather.Humidity);
            Assert.AreEqual("Sunny", weather.Precipitation);
            Assert.AreEqual("1.2m/s", weather.WindSpeed);
            Assert.AreEqual("170°", weather.WindDirection);
        }

        [TestMethod]
        public void PrinAllTest()
        {
            string name = "WeatherTests";
            string temperatureCelcius = "No Data";
            string temperatureFahrenheit = "10.4";
            string cloudCover = "27";
            string humidity = "45";
            string precipitation = "Sunny";
            string windSpeed = "1.2";
            string windDirection = "170";

            Weather weather = new Weather(name, temperatureCelcius, temperatureFahrenheit, cloudCover, humidity, precipitation, windSpeed, windDirection);

            weather.PrintAll();
        }
    }
}