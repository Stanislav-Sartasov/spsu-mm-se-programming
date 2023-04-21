using DataParsers;
using IoC;
using NUnit.Framework;
using OpenweatherWeatherForecast;
using StormglassWeatherForecast;

namespace IoCTests
{
    public class IocTest
    {
        private ContainerConfig container;

        [SetUp]
        public void Setup()
        {
            container = new ContainerConfig();
        }

        [Test]
        public void GetRightWeatherForecastTest()
        {
            Assert.AreEqual(0, container.WeatherForecasts.Count);

            container.AddService<StormglassForecast, StormglassParser>("key", 0);

            Assert.AreEqual(typeof(StormglassForecast), container.GetWeatherForecast<StormglassForecast>().GetType());
        }

        [Test]
        public void AddWeatherForecastTest()
        {
            container.AddService<StormglassForecast, StormglassParser>("key", 0);
            container.AddService<OpenweatherForecast, OpenweatherParser>("key", 1);
            
            Assert.IsNotNull(container.GetWeatherForecast<OpenweatherForecast>());
            Assert.IsNotNull(container.GetWeatherForecast<StormglassForecast>());
            Assert.AreEqual(2, container.WeatherForecasts.Count);
        }

        [Test]
        public void AddExistWeatherForecastTest()
        {
            container.AddService<StormglassForecast, StormglassParser>("key", 0);

            container.AddService<StormglassForecast, StormglassParser>("key", 0);

            Assert.AreEqual(1, container.WeatherForecasts.Count);
        }

        [Test]
        public void RemoveWeatherForecastTest()
        {
            container.AddService<StormglassForecast, StormglassParser>("key", 0);
            container.AddService<OpenweatherForecast, OpenweatherParser>("key", 1);

            container.RemoveService<StormglassForecast>();
            Assert.AreEqual(1, container.WeatherForecasts.Count);

            container.RemoveService<StormglassForecast>();
            Assert.AreEqual(1, container.WeatherForecasts.Count);
        }

        [Test]
        public void IsActiveWeatherForecastTest()
        {
            container.AddService<StormglassForecast, StormglassParser>("key", 0);
            Assert.IsTrue(container.IsServiceActive<StormglassForecast>());

            container.RemoveService<StormglassForecast>();
            Assert.IsFalse(container.IsServiceActive<StormglassForecast>());
        }
    }
}