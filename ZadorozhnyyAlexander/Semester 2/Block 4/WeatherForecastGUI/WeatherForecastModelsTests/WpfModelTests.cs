using AbstractWeatherForecast;
using NUnit.Framework;
using WeatherForecastModelGUI;
using WpfWeatherForecastModel;

namespace WeatherForecastModelsTests
{
    public class WpfModelTests
    {
        private WpfWeatherForecastModel.WeatherForecastModel model;

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
            model = new WpfWeatherForecastModel.WeatherForecastModel();
        }

        [Test]
        public void UpdateTest()
        {
            model.UpdateCommand.Execute(null);
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

            model.SwitchCommand.Execute(null);

            Assert.AreEqual(SiteTypes.Stormglass, model.SiteType);
            Assert.AreEqual("Weather Forecast from site Stormglass", model.Description);

            model.SwitchCommand.Execute(null);
        }

        [Test]
        public void UpdateServiceActivityStatus()
        {
            model.ActivityCommand.Execute(null);
            Assert.IsFalse(model.IsUpdateEnabled);
            Assert.AreEqual(data, model.Data);

            model.ActivityCommand.Execute(null);
            Assert.IsTrue(model.IsUpdateEnabled);
            Assert.AreNotEqual(data, model.Data);
        }
    }
}