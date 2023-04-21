using NUnit.Framework;
using System;
using System.Reflection;
using SiteInterfaces;
using System.Collections.Generic;

namespace Sites.UnitTests
{
    public class ParsingSitesTests
    {
        [Test]
        public void OpenWeatherMapGoodParseTest()
        {
            var json = "{\"coord\":{\"lon\":30.2642,\"lat\":59.8944},\"weather\":[{\"id\":803,\"main\":\"tears\",\"description\":\"broken tears\",\"icon\":\"04n\"}],\"base\":\"stations\",\"main\":{\"temp\":5,\"feels_like\":-1.91,\"temp_min\":2.75,\"temp_max\":3.08,\"pressure\":1008,\"humidity\":5},\"visibility\":10000,\"wind\":{\"speed\":3,\"deg\":5},\"clouds\":{\"all\":5},\"dt\":1651623872,\"sys\":{\"type\":2,\"id\":197864,\"country\":\"RU\",\"sunrise\":1651628986,\"sunset\":1651687297},\"timezone\":10800,\"id\":498817,\"name\":\"Saint Petersburg\",\"cod\":200}";
            var site = new OpenWeatherMap();
            IsParseGoodData(site, json);
            Assert.AreEqual("tears, description: broken tears", site.WeatherInfo.Precipitation);
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(1651623872).ToLocalTime();
            Assert.AreEqual(dateTime, site.WeatherInfo.Time);
        }

        [Test]
        public void TomorrowIOGoodParseTest()
        {
            var json = "{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":\"2022-05-04T03:51:08.414306786+03:00\",\"startTime\":\"2022-05-04T03:51:08.414306786+03:00\",\"intervals\":[{\"startTime\":\"2022-05-04T03:51:08.414306786+03:00\",\"values\":{\"cloudCover\":5,\"humidity\":5,\"precipitationIntensity\":0,\"precipitationProbability\":0,\"precipitationType\":0,\"temperature\":5,\"windDirection\":5,\"windSpeed\":3}}]}]}}";
            var site = new TomorrowIO();
            IsParseGoodData(site, json);
            Assert.AreEqual("Precipitation type: Clear, precipitation intensity: 0 mm/hr, precipitation probability: 0 %", site.WeatherInfo.Precipitation);
            DateTime dateTime = DateTime.Parse("2022-05-04T03:51:08.414306786+03:00", null, System.Globalization.DateTimeStyles.RoundtripKind);
            Assert.AreEqual(dateTime, site.WeatherInfo.Time);
        }

        [Test]
        public void StormGlassIOGoodParseTest()
        {
            var json = "{\"hours\":[{\"airTemperature\":{\"dwd\":-0.09,\"noaa\":5,\"sg\":-0.09},\"cloudCover\":{\"dwd\":80.41,\"noaa\":5,\"sg\":80.41},\"humidity\":{\"dwd\":61.52,\"noaa\":5,\"sg\":61.52},\"precipitation\":{\"dwd\":0.0,\"noaa\":0,\"sg\":0.0},\"time\":\"2022-05-04T01:00:00+00:00\",\"windDirection\":{\"icon\":317.02,\"noaa\":5,\"sg\":317.02},\"windSpeed\":{\"icon\":7.43,\"noaa\":3,\"sg\":7.43}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":\"2022-05-04 01:07\",\"lat\":59.93863,\"lng\":30.31413,\"params\":[\"windDirection\",\"windSpeed\",\"airTemperature\",\"cloudCover\",\"humidity\",\"precipitation\"],\"requestCount\":3,\"start\":\"2022-05-04 01:00\"}}";
            var site = new StormGlassIO();
            IsParseGoodData(site, json);
            Assert.AreEqual("Precipitation intensity: 0 mm/hr", site.WeatherInfo.Precipitation);
            DateTime dateTime = DateTime.Parse("2022-05-04T01:00:00+00:00", null, System.Globalization.DateTimeStyles.RoundtripKind);
            Assert.AreEqual(dateTime, site.WeatherInfo.Time);
        }

        [Test]
        public void BadParseTest()
        {
            var json = "AHAHAHAHAAHHAAHHAAHAHAHAHAHAAHAHHAHAHAHAHAHAHA";
            var sites = new List<ISite>();
            sites.Add(new OpenWeatherMap());
            sites.Add(new TomorrowIO());
            sites.Add(new StormGlassIO());
            for (int i = 0; i < 3; i++)
            {
                typeof(ASite).GetProperty("response", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sites[i], json);
                typeof(ASite).GetProperty("requestSuccess", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sites[i], false);
                Assert.IsFalse(sites[i].Parse());
                typeof(ASite).GetProperty("requestSuccess", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(sites[i], true);
                Assert.IsFalse(sites[i].Parse());
            }

        }

        private void IsParseGoodData(ASite site, string json)
        {
            typeof(ASite).GetProperty("response", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(site, json);
            typeof(ASite).GetProperty("requestSuccess", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(site, true);
            Assert.IsTrue(site.Parse());
            Assert.AreEqual(5, site.WeatherInfo.TemperatureInCelsius);
            Assert.AreEqual(5, site.WeatherInfo.CloudCover);
            Assert.AreEqual(5, site.WeatherInfo.Humidity);
            Assert.AreEqual(5, site.WeatherInfo.WindDirection);
            Assert.AreEqual(3, site.WeatherInfo.WindSpeed);
            Assert.AreEqual(site.WeatherInfo.TemperatureInCelsius * 1.8 + 32, site.WeatherInfo.TemperatureInFahrenheit);
        }
    }
}
