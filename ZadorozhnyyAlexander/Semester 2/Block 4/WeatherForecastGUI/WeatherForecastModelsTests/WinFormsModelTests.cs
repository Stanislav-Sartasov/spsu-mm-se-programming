using NUnit.Framework;
using WeatherForecastModelGUI;
using AbstractWeatherForecast;


namespace WeatherForecastModelsTests
{
    public class WinFormsModelTests
    {
        private WeatherForecastModel model;

        private readonly string data = $"Celsius Temperature : Nothing.\n" +
                $"Fahrenheit Temperature : Nothing.\n" +
                $"Cloud Cover : Nothing.\n" +
                $"Precipitation : Nothing.\n" +
                $"Air Humidity : Nothing.\n" +
                $"Wild Direction : Nothing.\n" +
                $"Wild Speed : Nothing.";

        [SetUp]
        public void Setup()
        {
            model = new WeatherForecastModel();
        }

        [Test]
        public void UpdateTest()
        {
            model.UpdateData();
            Assert.IsNotNull(model);
            Assert.IsTrue(model.IsUpdateEnabled);
            Assert.AreEqual("Weather Forecast from site Openweather", model.Description);
            Assert.AreNotEqual(data, model.Data);
        }

        [Test]
        public void SwitchTest()
        {
            Assert.AreEqual(SiteTypes.Openweather, model.SiteType);
            Assert.AreEqual("Weather Forecast from site Openweather", model.Description);

            model.SwitchService();

            Assert.AreEqual(SiteTypes.Stormglass, model.SiteType);
            Assert.AreEqual("Weather Forecast from site Stormglass", model.Description);

            model.SwitchService();
        }

        [Test]
        public void UpdateServiceActivityStatus()
        {
            model.UpdateServiceActivityStatus();
            Assert.IsFalse(model.IsUpdateEnabled);
            Assert.AreEqual(data, model.Data);

            model.UpdateServiceActivityStatus();
            Assert.IsTrue(model.IsUpdateEnabled);
            Assert.AreNotEqual(data, model.Data);
        }
    }
}