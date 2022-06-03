using NUnit.Framework;
using JsonParsingLibrary;
using System.Collections.Generic;
using WeatherClasses;

namespace WeatherClasses.Tests
{
    public class WeatherAppTests
    {
        [Test]
        public void ShowWeatherTest()
        {
            var tomorrowWebHelper = new Moq.Mock<IWebServerHelper>();
            var testReader = new Moq.Mock<IResponseReader>();
            string tomorrowJson = "{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":" + 
                "\"2022 - 04 - 23T08: 38:00Z\",\"startTime\":\"2022 - 04 - 23T08: 38:00Z\",\"intervals\"" + 
                ":[{\"startTime\":\"2022 - 04 - 23T08: 38:00Z\",\"values\":{\"cloudCover\":100" + 
                ",\"humidity\":55,\"precipitationIntensity\":0,\"temperature\":9.88," +
                "\"windDirection\":37.69,\"windSpeed\":5.5}}]}]}}";
            testReader.Setup(x => x.GetResponseInfo()).Returns(tomorrowJson);
            tomorrowWebHelper.Setup(x => x.MakeRequest()).Returns(true);
            var stormglassWebHelper = new Moq.Mock<IWebServerHelper>();
            string stormglassJson = "{\"hours\":[{\"airTemperature\":{\"noaa\":8.76},\"cloudCover\"" +
                ":{\"noaa\":99.63},\"humidity\":{\"noaa\":59.07},\"precipitation\":{\"noaa\":0.0}," +
                "\"time\":\"2022 - 04 - 23T08: 00:00 + 00:00\",\"windDirection\":{\"noaa\":41.56}," +
                "\"windSpeed\":{\"noaa\":3.42}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":" +
                "\"2022 - 04 - 23 08:37\",\"lat\":59.57,\"lng\":30.19,\"params\":[\"airTemperature" +
                "\",\"cloudCover\",\"humidity\",\"precipitation\",\"windDirection\",\"windSpeed\"]," +
                "\"requestCount\":1,\"source\":[\"noaa\"],\"start\":\"2022 - 04 - 23 08:00\"}}";
            stormglassWebHelper.Setup(x => x.MakeRequest()).Returns(true);

            TomorrowParametersProvider tomorrowParametersProvider = new TomorrowParametersProvider(testReader.Object, tomorrowWebHelper.Object);
            tomorrowParametersProvider.FillWeatherProperties();

            testReader.Setup(x => x.GetResponseInfo()).Returns(stormglassJson);

            StormglassParametersProvider stormglassParametersProvider = new StormglassParametersProvider(testReader.Object, stormglassWebHelper.Object);
            stormglassParametersProvider.FillWeatherProperties();

            double temperature = 9.88;
            double fahrenheitTemperature = temperature * ((double)9 / (double)5) + 32;
            string strFTemperature = fahrenheitTemperature.ToString("0.##");
            strFTemperature = strFTemperature.Replace(',', '.');

            WeatherCharacterization tomorrowCharacterization = new WeatherCharacterization()
            {
                Site = "tomorrow.io",
                CloudCover = "100 in %",
                Humidity = "55 in %",
                WindSpeed = "5.5 in m/s",
                WindDirection = "37.69 in degrees",
                Precipitation = "0 in %",
                Temperature = string.Format("{0} in Celsius, {1} in Fahrenheits",
                    "9.88", strFTemperature)
            };

            temperature = 8.76;
            fahrenheitTemperature = temperature * ((double)9 / (double)5) + 32;
            strFTemperature = fahrenheitTemperature.ToString("0.##");
            strFTemperature = strFTemperature.Replace(',', '.');

            WeatherCharacterization stormglassCharacterization = new WeatherCharacterization()
            {
                Site = "stormglass.io",
                CloudCover = "99.63 in %",
                Humidity = "59.07 in %",
                WindSpeed = "3.42 in m/s",
                WindDirection = "41.56 in degrees",
                Precipitation = "0.0 in %",
                Temperature = string.Format("{0} in Celsius, {1} in Fahrenheits",
                    "8.76", strFTemperature)
            };

            EqualityCheck(tomorrowParametersProvider.Weather, tomorrowCharacterization);
            EqualityCheck(stormglassParametersProvider.Weather, stormglassCharacterization);
        }

        public void EqualityCheck(WeatherCharacterization programmResult, WeatherCharacterization testResult)
        {
            Assert.AreEqual(programmResult.Temperature, testResult.Temperature);
            Assert.AreEqual(programmResult.Site, testResult.Site);
            Assert.AreEqual(programmResult.WindSpeed, testResult.WindSpeed);
            Assert.AreEqual(programmResult.WindDirection, testResult.WindDirection);
            Assert.AreEqual(programmResult.Humidity, testResult.Humidity);
            Assert.AreEqual(programmResult.Precipitation, testResult.Precipitation);
            Assert.AreEqual(programmResult.CloudCover, testResult.CloudCover);
        }

        [Test]
        public void ChangeWeatherTest()
        {
            WeatherCharacterization stormglassCharacterization = new WeatherCharacterization()
            {
                Site = "stormglass.io",
                CloudCover = "99.63 in %",
                Humidity = "59.07 in %",
                WindSpeed = "3.42 in m/s",
                WindDirection = "41.56 in degrees",
                Precipitation = "0.0 in %",
                Temperature = "5.5"
            };

            WeatherCharacterization tomorrowCharacterization = new WeatherCharacterization()
            {
                Site = "tomorrow.io",
                CloudCover = "100 in %",
                Humidity = "55 in %",
                WindSpeed = "5.5 in m/s",
                WindDirection = "37.69 in degrees",
                Precipitation = "0 in %",
                Temperature = "7.7"
            };

            tomorrowCharacterization.ChangeWeather(stormglassCharacterization);

            EqualityCheck(tomorrowCharacterization, stormglassCharacterization);
        }
    }
}