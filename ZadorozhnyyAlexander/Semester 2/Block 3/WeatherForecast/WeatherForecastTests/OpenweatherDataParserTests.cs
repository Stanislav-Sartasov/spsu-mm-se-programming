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
    public class OpenweatherDataParserTests
    {
        private HttpClient httpClient;
        private MockHttpHelper mockHelper;

        private ApiHelper apiHelper;
        private AParser parser;

        private const string key = "test";
        private const string url = "http://api.openweathermap.org/data/2.5/weather";

        private string[] parameters = {
                $"?lat=59.93863",
                $"&lon=30.31413",
                $"&appid={key}",
                "&units=metric"
            };

        private List<string> rightAnswer = new List<string>() { "1", "33.8", "1", "No data", "1", "1", "1" };

        [SetUp]
        public void Setup()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            mockHelper = new MockHttpHelper();

            httpClient = new HttpClient(mockHelper.Object());
        }

        [Test]
        public void OpenweatherParserRightAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""coord"":{""lon"":30.3141,""lat"":59.9386},""weather"":[{""id"":803,""main"":""Clouds"",""description"":""broken clouds"",""icon"":""04n""}],""base"":""stations"",""main"":{""temp"":1,""feels_like"":0.18,""temp_min"":4.44,""temp_max"":5.05,""pressure"":1009,""humidity"":1},""visibility"":10000,""wind"":{""speed"":1,""deg"":1},""clouds"":{""all"":1},""dt"":1651003007,""sys"":{""type"":2,""id"":197864,""country"":""RU"",""sunrise"":1650939065,""sunset"":1650994906},""timezone"":10800,""id"":519690,""name"":""Novaya Gollandiya"",""cod"":200}");

            apiHelper = new ApiHelper(parameters, key, url, (int)SiteTypes.Openweather, httpClient);
            parser = new OpenweatherParser(apiHelper);

            Assert.AreEqual(parser.GetListOfCurrentData(), rightAnswer);
        }

        [Test]
        public void OpenweatherParserFalseAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""cod"": ""502"", ""message"": ""The site is not available at the moment.""}", System.Net.HttpStatusCode.BadGateway);

            apiHelper = new ApiHelper(parameters, key, url, (int)SiteTypes.Openweather, httpClient);
            parser = new OpenweatherParser(apiHelper);

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