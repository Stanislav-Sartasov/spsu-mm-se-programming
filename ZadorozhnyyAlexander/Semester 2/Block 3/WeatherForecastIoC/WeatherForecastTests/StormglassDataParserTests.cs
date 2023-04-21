using NUnit.Framework;
using RequestApi;
using MockHttpObject;
using System.Net.Http;
using DataParsers;
using AbstractWeatherForecast;
using System;
using System.Collections.Generic;

namespace WeatherForecastTests
{
    public class StormglassDataParserTests
    {
        private HttpClient httpClient;
        private MockHttpHelper mockHelper;

        private ApiHelper apiHelper;
        private AParser parser;

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
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-Us");

            mockHelper = new MockHttpHelper();

            httpClient = new HttpClient(mockHelper.Object());
        }

        [Test]
        public void StormglassParserRightAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""hours"":[{""airTemperature"":{""sg"":1},""cloudCover"":{""sg"":1},""humidity"":{""sg"":1},""precipitation"":{""sg"":1},""windDirection"":{""sg"":1},""windSpeed"":{""sg"":1}}]}");

            apiHelper = new ApiHelper(parameters, key, url, (int)SiteTypes.Stormglass, httpClient);
            parser = new StormglassParser(apiHelper);

            Assert.AreEqual(parser.GetListOfCurrentData(), rightAnswer);
        }

        [Test]
        public void StormglassParserFalseAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""StatusCode"": ""502"", ""message"": ""The site is not available at the moment.""}", System.Net.HttpStatusCode.BadGateway);

            apiHelper = new ApiHelper(parameters, key, url, (int)SiteTypes.Stormglass, httpClient);
            parser = new StormglassParser(apiHelper);

            try
            {
                parser.GetListOfCurrentData();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Bad Gateway");
            }
        }
    }
}