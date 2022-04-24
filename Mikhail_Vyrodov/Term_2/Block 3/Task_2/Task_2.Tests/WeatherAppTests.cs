using NUnit.Framework;
using JsonParsingLibrary;
using System;
using System.Collections.Generic;
namespace Task_2.Tests
{
    public class WeatherAppTests
    {
        [Test]
        public void ShowWeatherTest()
        {
            var tomorrowParser = new Moq.Mock<IWebServerHelper>();
            JSONParser jsonParser = new JSONParser();
            string tomorrowJson = "{\"data\":{\"timelines\":[{\"timestep\":\"current\",\"endTime\":" + 
                "\"2022 - 04 - 23T08: 38:00Z\",\"startTime\":\"2022 - 04 - 23T08: 38:00Z\",\"intervals\"" + 
                ":[{\"startTime\":\"2022 - 04 - 23T08: 38:00Z\",\"values\":{\"cloudCover\":100" + 
                ",\"humidity\":55,\"precipitationIntensity\":0,\"temperature\":9.88," +
                "\"windDirection\":37.69,\"windSpeed\":5.5}}]}]}}";
            tomorrowParser.Setup(x => x.GetJSON()).Returns(tomorrowJson);
            tomorrowParser.Setup(x => x.MakeRequest()).Returns(true);
            tomorrowParser.Setup(x => x.Site).Returns("tomorrow.io");
            ConsoleWriter consoleWriter = new ConsoleWriter();
            string answer = consoleWriter.ShowSiteWeather(tomorrowParser.Object);
            Dictionary<string, string> testParameters = new Dictionary<string, string>();
            double temperature = 9.88;
            double fahrenheitTemperature = temperature * (9 / 5) + 32;
            testParameters.Add("cloudCover", "100");
            testParameters.Add("temperature", "9.88");
            testParameters.Add("fahrenheitTemperature", fahrenheitTemperature.ToString("0.##"));
            testParameters.Add("humidity", "55");
            testParameters.Add("precipitation", "0");
            testParameters.Add("windSpeed", "5.5");
            testParameters.Add("windDirection", "37.69");
            testParameters.Add("site", "tomorrow.io");

            string testAnswer = "";
            testAnswer += string.Format("This information is from {0}\n", testParameters["site"]);
            testAnswer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n", testParameters["temperature"], testParameters["fahrenheitTemperature"]);
            testAnswer += string.Format("Humidity in percents - {0}\n", testParameters["humidity"]);
            testAnswer += string.Format("Cloud cover in percents - {0}\n", testParameters["cloudCover"]);
            testAnswer += string.Format("Wind speed in m/s - {0}\n", testParameters["windSpeed"]);
            testAnswer += string.Format("Wind direction in degrees - {0}\n\n", testParameters["windDirection"]);
            Assert.AreEqual(testAnswer, answer);
            Assert.AreEqual(testParameters, consoleWriter.Parameters);

            testParameters = new Dictionary<string, string>();
            var stormglassParser = new Moq.Mock<IWebServerHelper>();
            string stormglassJson = "{\"hours\":[{\"airTemperature\":{\"noaa\":8.76},\"cloudCover\"" + 
                ":{\"noaa\":99.63},\"humidity\":{\"noaa\":59.07},\"precipitation\":{\"noaa\":0.0}," + 
                "\"time\":\"2022 - 04 - 23T08: 00:00 + 00:00\",\"windDirection\":{\"noaa\":41.56}," + 
                "\"windSpeed\":{\"noaa\":3.42}}],\"meta\":{\"cost\":1,\"dailyQuota\":10,\"end\":" + 
                "\"2022 - 04 - 23 08:37\",\"lat\":59.57,\"lng\":30.19,\"params\":[\"airTemperature" + 
                "\",\"cloudCover\",\"humidity\",\"precipitation\",\"windDirection\",\"windSpeed\"]," + 
                "\"requestCount\":1,\"source\":[\"noaa\"],\"start\":\"2022 - 04 - 23 08:00\"}}";
            stormglassParser.Setup(x => x.GetJSON()).Returns(stormglassJson);
            stormglassParser.Setup(x => x.MakeRequest()).Returns(true);
            stormglassParser.Setup(x => x.Site).Returns("stormglass.io");
            temperature = 8.76;
            fahrenheitTemperature = temperature * (9 / 5) + 32;
            testParameters.Add("cloudCover", "99.63");
            testParameters.Add("temperature", "8.76");
            testParameters.Add("fahrenheitTemperature", fahrenheitTemperature.ToString("0.##"));
            testParameters.Add("humidity", "59.07");
            testParameters.Add("precipitation", "0.0");
            testParameters.Add("windSpeed", "3.42");
            testParameters.Add("windDirection", "41.56");
            testParameters.Add("site", "stormglass.io");

            testAnswer = "";
            testAnswer += string.Format("This information is from {0}\n", testParameters["site"]);
            testAnswer += string.Format("Air temperature in Celsius - {0}, in Fahrenheits - {1}\n", testParameters["temperature"], testParameters["fahrenheitTemperature"]);
            testAnswer += string.Format("Humidity in percents - {0}\n", testParameters["humidity"]);
            testAnswer += string.Format("Cloud cover in percents - {0}\n", testParameters["cloudCover"]);
            testAnswer += string.Format("Wind speed in m/s - {0}\n", testParameters["windSpeed"]);
            testAnswer += string.Format("Wind direction in degrees - {0}\n\n", testParameters["windDirection"]);
            answer = consoleWriter.ShowSiteWeather(stormglassParser.Object);
            Assert.AreEqual(testAnswer, answer);
            Assert.AreEqual(testParameters, consoleWriter.Parameters);
        }

        [Test]
        public void GetRequestURLTest()
        {
            Uri correctTomorrowioRequestURL = new Uri("https://api.tomorrow.io/v4/timelines?location=59.57,30.19&fields=temperature,cloudCover,humidity,precipitationIntensity,windDirection,windSpeed&timesteps=current&units=metric&apikey=AVMNJMtSlSrsgXtt1gIB6x2MrgKqIqxO");
            RequestURLGetter urlGetter = new RequestURLGetter();
            Uri testTomorrowioRequestURL = urlGetter.GetRequestURL("tomorrow.io");
            Assert.AreEqual(correctTomorrowioRequestURL, testTomorrowioRequestURL);
            var timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Uri initialStormglassioRequestURL = new Uri("https://api.stormglass.io/v2/weather/point");
            string stormglassParameters = string.Format("?lat=59.57&lng=30.19&params=airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed&start={0}&end={1}&source=noaa", timeStamp, timeStamp);
            Uri correctStormglassioRequestURL = new Uri(initialStormglassioRequestURL, stormglassParameters);
            Uri testStormglassioRequestURL = urlGetter.GetRequestURL("stormglass.io");
            Assert.AreEqual(correctStormglassioRequestURL, testStormglassioRequestURL);
        }
    }
}