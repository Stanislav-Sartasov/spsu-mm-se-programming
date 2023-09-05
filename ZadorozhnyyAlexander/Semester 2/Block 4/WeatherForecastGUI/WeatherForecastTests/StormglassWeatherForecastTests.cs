using NUnit.Framework;
using RequestApi;
using MockHttpObject;
using System.Net.Http;
using DataParsers;
using AbstractWeatherForecast;
using System;
using System.Reflection;
using System.Collections.Generic;
using StormglassWeatherForecast;

namespace WeatherForecastTests
{
    public class StormglassWeatherForecastTests
    {
        private AParser parser;
        private MockHttpHelper mockHelper;

        private const string key = "test";
        private const string url = "https://api.stormglass.io/v2/weather/point";
        private string[] parameters = {
                "?lat=59.93863",
                "&lng=30.31413",
                "&params=",
                "airTemperature,cloudCover,humidity,precipitation,windDirection,windSpeed",
                $"&start={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                $"&end={((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds()}",
                "&source=sg",
            };

        private List<string> rightAnswer = new List<string>() { "1", "33.8", "1", "1", "1", "1", "1" };

        [SetUp]
        public void Setup()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            mockHelper = new MockHttpHelper();

            var httpClient = new HttpClient(mockHelper.Object());

            parser = new StormglassParser(new ApiHelper(parameters, key, url, (int)SiteTypes.Openweather, httpClient));
        }

        [Test]
        public void StormglassParserRightAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""hours"":[{""airTemperature"":{""sg"":1},""cloudCover"":{""sg"":1},""humidity"":{""sg"":1},""precipitation"":{""sg"":1},""windDirection"":{""sg"":1},""windSpeed"":{""sg"":1}}]}");

            AWeatherForecast rightWeather = new StormglassForecast(parser);

            int index = 0;
            foreach (PropertyInfo info in rightWeather.GetType().GetProperties())
            {
                info.SetValue(rightWeather, rightAnswer[index]);
                index++;
            }
                
            AWeatherForecast testedWeather = new StormglassForecast(parser);
            testedWeather.Update();

            foreach (PropertyInfo info in rightWeather.GetType().GetProperties())
            {
                Assert.AreEqual(info.GetValue(rightWeather), info.GetValue(testedWeather));
            }
        }

        [Test]
        public void StormglassParserFalseAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""StatusCode"":""502"", ""ReasonPhrase"":""Bad Gateway""", System.Net.HttpStatusCode.BadGateway);

            AWeatherForecast testedWeather = new StormglassForecast(parser);

            try
            {
                testedWeather.Update();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Bad Gateway");
            }
        }

        [Test]
        public void StormglassShowDescriptionTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""hours"":[{""airTemperature"":{""sg"":1},""cloudCover"":{""sg"":1},""humidity"":{""sg"":1},""precipitation"":{""sg"":1},""windDirection"":{""sg"":1},""windSpeed"":{""sg"":1}}]}");

            AWeatherForecast testedWeather = new StormglassForecast(parser);
            testedWeather.Update();

            try
            {
                testedWeather.ShowFullWeatherForecast();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
