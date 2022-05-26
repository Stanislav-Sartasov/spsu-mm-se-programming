using NUnit.Framework;
using JsonParsingLibrary;
using System.Collections.Generic;

namespace Task_3.Tests
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
            TomorrowioMapper tomorrowMapper = new TomorrowioMapper();
            TomorrowioRequestURLFiller tomorrowRequestFiller = new TomorrowioRequestURLFiller();
            TomorrowioParametersFiller tomorrowParamsFiller = new TomorrowioParametersFiller(tomorrowRequestFiller, tomorrowMapper);
            ConsoleWriter writer = new ConsoleWriter();
            TomorrowioWeatherDisplayer tomorrowDisplayer = new TomorrowioWeatherDisplayer(tomorrowWebHelper.Object,
                tomorrowParamsFiller, testReader.Object, writer);
            var stormglassWebHelper = new Moq.Mock<IWebServerHelper>();
            string stormglassJson = "{\"hours\":[{\"airTemperature\":{\"noaa\":8.76},\"cloudCover\"" +
                ":{\"noaa\":99.63},\"humidity\":{\"noaa\":59.07},\"precipitation\":{\"noaa\":0.0}," +
                "\"time\":\"2022 - 04 - 23T08: 00:00 + 00:00\",\"windDirection\":{\"noaa\":41.56}," +
                "\"windSpeed\":{\"noaa\":3.42}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":" +
                "\"2022 - 04 - 23 08:37\",\"lat\":59.57,\"lng\":30.19,\"params\":[\"airTemperature" +
                "\",\"cloudCover\",\"humidity\",\"precipitation\",\"windDirection\",\"windSpeed\"]," +
                "\"requestCount\":1,\"source\":[\"noaa\"],\"start\":\"2022 - 04 - 23 08:00\"}}";
            stormglassWebHelper.Setup(x => x.MakeRequest()).Returns(true);
            StormglassioMapper stormglassMaper = new StormglassioMapper();
            StormglassioRequestURLFiller stormglassRequestFiller = new StormglassioRequestURLFiller();
            StormglassioParametersFiller stormglassParamsFiller = new StormglassioParametersFiller(stormglassRequestFiller, stormglassMaper);
            StormglassioWeatherDisplayer stormglassDisplayer = new StormglassioWeatherDisplayer(stormglassWebHelper.Object,
                stormglassParamsFiller, testReader.Object, writer);
            tomorrowDisplayer.DisplayWeather();
            testReader.Setup(x => x.GetResponseInfo()).Returns(stormglassJson);
            stormglassDisplayer.DisplayWeather();
            string tomorrowAnswer = tomorrowDisplayer.Answer;
            string stormglassAnswer = stormglassDisplayer.Answer;
            Dictionary<string, string> testParameters = new Dictionary<string, string>();
            double temperature = 9.88;
            double fahrenheitTemperature = temperature * ((double)9 / (double)5) + 32;
            string strFTemperature = fahrenheitTemperature.ToString("0.##");
            strFTemperature = strFTemperature.Replace(',', '.');
            testParameters.Add("cloudCover", "100");
            testParameters.Add("temperature", "9.88");
            testParameters.Add("fahrenheitTemperature", strFTemperature);
            testParameters.Add("humidity", "55");
            testParameters.Add("precipitation", "0");
            testParameters.Add("windSpeed", "5.5");
            testParameters.Add("windDirection", "37.69");
            testParameters.Add("site", "tomorrow.io");

            string testAnswer = "";
            testAnswer += string.Format("This information is from {0}\n", testParameters["site"]);
            testAnswer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n",
                testParameters["temperature"], testParameters["fahrenheitTemperature"]);
            testAnswer += string.Format("Humidity in percents - {0}\n", testParameters["humidity"]);
            testAnswer += string.Format("Cloud cover in percents - {0}\n", testParameters["cloudCover"]);
            testAnswer += string.Format("Wind speed in m/s - {0}\n", testParameters["windSpeed"]);
            testAnswer += string.Format("Wind direction in degrees - {0}\n\n", testParameters["windDirection"]);
            Assert.AreEqual(testParameters, tomorrowDisplayer.ParamsFiller.Parameters);
            Assert.AreEqual(testAnswer, tomorrowAnswer);

            testParameters = new Dictionary<string, string>();
            temperature = 8.76;
            fahrenheitTemperature = temperature * ((double)9 / (double)5) + 32;
            strFTemperature = fahrenheitTemperature.ToString("0.##");
            strFTemperature = strFTemperature.Replace(',', '.');
            testParameters.Add("cloudCover", "99.63");
            testParameters.Add("temperature", "8.76");
            testParameters.Add("fahrenheitTemperature", strFTemperature);
            testParameters.Add("humidity", "59.07");
            testParameters.Add("precipitation", "0.0");
            testParameters.Add("windSpeed", "3.42");
            testParameters.Add("windDirection", "41.56");
            testParameters.Add("site", "stormglass.io");

            testAnswer = "";
            testAnswer += string.Format("This information is from {0}\n", testParameters["site"]);
            testAnswer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n",
                testParameters["temperature"], testParameters["fahrenheitTemperature"]);
            testAnswer += string.Format("Humidity in percents - {0}\n", testParameters["humidity"]);
            testAnswer += string.Format("Cloud cover in percents - {0}\n", testParameters["cloudCover"]);
            testAnswer += string.Format("Wind speed in m/s - {0}\n", testParameters["windSpeed"]);
            testAnswer += string.Format("Wind direction in degrees - {0}\n\n", testParameters["windDirection"]);
            Assert.AreEqual(testAnswer, stormglassAnswer);
            Assert.AreEqual(testParameters, stormglassDisplayer.ParamsFiller.Parameters);

            testAnswer = "tomorrow.io is currently unavailable";
            tomorrowWebHelper.Setup(x => x.MakeRequest()).Returns(false);
            tomorrowWebHelper.Setup(x => x.Answer).Returns(testAnswer);
            tomorrowDisplayer = new TomorrowioWeatherDisplayer(tomorrowWebHelper.Object,
                tomorrowParamsFiller, testReader.Object, writer);
            Assert.AreEqual(tomorrowDisplayer.DisplayWeather(), false);
            Assert.AreEqual(tomorrowDisplayer.ParamsFiller.Answer, testAnswer);

            testAnswer = "stormglass.io is currently unavailable";
            stormglassWebHelper.Setup(x => x.MakeRequest()).Returns(false);
            stormglassWebHelper.Setup(x => x.Answer).Returns(testAnswer);
            stormglassDisplayer = new StormglassioWeatherDisplayer(stormglassWebHelper.Object,
                stormglassParamsFiller, testReader.Object, writer);
            Assert.AreEqual(stormglassDisplayer.DisplayWeather(), false);
            Assert.AreEqual(stormglassDisplayer.ParamsFiller.Answer, testAnswer);
        }

        [Test]
        public void IoCContainterTest()
        {
            IoCContainer testContainer = new IoCContainer();
            List<IWeatherDisplayer> displayers = testContainer.GetDisplayers();
            Assert.AreEqual(displayers[0].GetType(), typeof(TomorrowioWeatherDisplayer));
            Assert.AreEqual(displayers[0].WebHelper.GetType(), typeof(TomorrowioWebHelper));
            Assert.AreEqual(displayers[0].RespReader.GetType(), typeof(ResponseReader));
            Assert.AreEqual(displayers[0].Writer.GetType(), typeof(ConsoleWriter));

            Assert.AreEqual(displayers[1].GetType(), typeof(StormglassioWeatherDisplayer));
            Assert.AreEqual(displayers[1].WebHelper.GetType(), typeof(StormglassioWebHelper));
            Assert.AreEqual(displayers[1].RespReader.GetType(), typeof(ResponseReader));
            Assert.AreEqual(displayers[1].Writer.GetType(), typeof(ConsoleWriter));
        }
    }
}