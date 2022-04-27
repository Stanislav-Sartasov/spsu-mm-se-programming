using NUnit.Framework;
using MockHttpObject;
using System.Net.Http;
using AbstractWeatherForecast;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace WeatherForecastTests
{
    public class OpenweatherWeatherForecastTests
    {
        private HttpClient httpClient;
        private MockHttpHelper mockHelper;

        private const string key = "b13eb5267bf4c9746e2f70d69a172b94";
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
            mockHelper = new MockHttpHelper();

            httpClient = new HttpClient(mockHelper.Object());
        }

        [Test]
        public void OpenweatherRightAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""coord"":{""lon"":30.3141,""lat"":59.9386},""weather"":[{""id"":803,""main"":""Clouds"",""description"":""broken clouds"",""icon"":""04n""}],""base"":""stations"",""main"":{""temp"":1,""feels_like"":0.18,""temp_min"":4.44,""temp_max"":5.05,""pressure"":1009,""humidity"":1},""visibility"":10000,""wind"":{""speed"":1,""deg"":1},""clouds"":{""all"":1},""dt"":1651003007,""sys"":{""type"":2,""id"":197864,""country"":""RU"",""sunrise"":1650939065,""sunset"":1650994906},""timezone"":10800,""id"":519690,""name"":""Novaya Gollandiya"",""cod"":200}");

            AWeatherForecast rightWeather = new OpenweatherWeatherForecast.OpenweatherWeatherForecast();

            int index = 0;
            foreach (PropertyInfo info in rightWeather.GetType().GetProperties())
            {
                info.SetValue(rightWeather, rightAnswer[index]);
                index++;
            }

            AWeatherForecast testedWeather = new OpenweatherWeatherForecast.OpenweatherWeatherForecast();
            testedWeather.Initialize(httpClient);

            foreach (PropertyInfo info in rightWeather.GetType().GetProperties())
            {
                Assert.AreEqual(info.GetValue(rightWeather), info.GetValue(testedWeather));
            }
        }

        [Test]
        public void OpenweatherFalseAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""cod"": ""502"", ""message"": ""The site is not available at the moment.""}", System.Net.HttpStatusCode.BadGateway);

            AWeatherForecast testedWeather = new OpenweatherWeatherForecast.OpenweatherWeatherForecast();

            try
            {
                testedWeather.Initialize(httpClient);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Bad Gateway");
            }
        }

        [Test]
        public void OpenweatherShowDescriptionTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""coord"":{""lon"":30.3141,""lat"":59.9386},""weather"":[{""id"":803,""main"":""Clouds"",""description"":""broken clouds"",""icon"":""04n""}],""base"":""stations"",""main"":{""temp"":1,""feels_like"":0.18,""temp_min"":4.44,""temp_max"":5.05,""pressure"":1009,""humidity"":1},""visibility"":10000,""wind"":{""speed"":1,""deg"":1},""clouds"":{""all"":1},""dt"":1651003007,""sys"":{""type"":2,""id"":197864,""country"":""RU"",""sunrise"":1650939065,""sunset"":1650994906},""timezone"":10800,""id"":519690,""name"":""Novaya Gollandiya"",""cod"":200}");

            AWeatherForecast testedWeather = new OpenweatherWeatherForecast.OpenweatherWeatherForecast();
            testedWeather.Initialize(httpClient);

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
