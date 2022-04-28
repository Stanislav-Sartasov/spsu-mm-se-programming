using System;
using Container;
using Forecast;
using NUnit.Framework;
using System.Collections.Generic;
using Tools;

namespace ToolsTests
{
    public class Tests
    {
        [Test]
        public void ConvertWeatherToStringSuccefsulTest()
        {
            var day = new List<CityWeatherForecast>()
            {
                new CityWeatherForecast("10", "20", "80", "90", "5")
            };

            var forecast = new SiteWeatherForecast("SomeSite", "No errors", day);
            string result = $"\n--------------------\nSource: SomeSite\n\nData: {DateTime.Now.ToString("g")}\nCelsius temperature: 10°C\nFahrenheit temperature: 50°F" +
                            "\nHumidity: 80%\nCloud cover: 20%\nWind speed: 5m/s \nWind direction: 90°\n--------------------\n";

            Assert.AreEqual(Tool.ConvertWeatherToString(forecast), result);
        }

        [Test]
        public void ConvertWeatherToStringUnsuccefsulTest()
        {
            var forecast = new SiteWeatherForecast("SomeSite", "Sate is down.");
            string result = $"--------------------\n\nFailed to get data from SomeSite.\nError message: Sate is down.\n\n--------------------\n";

            Assert.AreEqual(Tool.ConvertWeatherToString(forecast), result);
        }

        [Test]
        public void CheckCommandTests()
        {
            var commands = new string[]
            {
                "sites"
            };

            Assert.IsTrue(Tool.CheckCommand(commands));

            commands = new string[]
            {
                "a", "lot", "of", "agrs"
            };

            Assert.IsFalse(Tool.CheckCommand(commands));

            commands = new string[]
            {
                "add", "stormglass"
            };

            Assert.IsTrue(Tool.CheckCommand(commands));

            commands = new string[]
            {
                "something"
            };

            Assert.IsFalse(Tool.CheckCommand(commands));
        }

        [Test]
        public void GetSiteTest()
        {
            string siteName = "openweather";
            Assert.AreEqual(Tool.GetSite(siteName), SitesName.OpenWeather);

            siteName = "tomorrowio";
            Assert.AreEqual(Tool.GetSite(siteName), SitesName.TomorrowIO);

            siteName = "stormglass";
            Assert.AreEqual(Tool.GetSite(siteName), SitesName.StormGlass);
        }
    }
}