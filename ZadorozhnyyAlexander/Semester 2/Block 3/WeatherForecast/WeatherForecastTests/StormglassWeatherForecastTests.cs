using NUnit.Framework;
using RequestApi;
using MockHttpObject;
using System.Net.Http;
using DataParsers;
using AbstractWeatherForecast;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace WeatherForecastTests
{
    public class StormglassWeatherForecastTests
    {
        private HttpClient httpClient;
        private MockHttpHelper mockHelper;

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
            mockHelper = new MockHttpHelper();

            httpClient = new HttpClient(mockHelper.Object());
        }

        [Test]
        public void StormglassParserRightAnswerTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""hours"":[{""airTemperature"":{""sg"":1},""cloudCover"":{""sg"":1},""humidity"":{""sg"":1},""precipitation"":{""sg"":1},""windDirection"":{""sg"":1},""windSpeed"":{""sg"":1}}]}");

            AWeatherForecast rightWeather = new StormglassWeatherForecast.StormglassWeatherForecast();

            int index = 0;
            foreach (PropertyInfo info in rightWeather.GetType().GetProperties())
            {
                info.SetValue(rightWeather, rightAnswer[index]);
                index++;
            }
                
            AWeatherForecast testedWeather = new StormglassWeatherForecast.StormglassWeatherForecast();
            testedWeather.Initialize(httpClient);

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

            AWeatherForecast testedWeather = new StormglassWeatherForecast.StormglassWeatherForecast();

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
        public void StormglassShowDescriptionTest()
        {
            mockHelper
            .When(url + String.Join("", parameters))
            .Respond(@"{""hours"":[{""airTemperature"":{""sg"":1},""cloudCover"":{""sg"":1},""humidity"":{""sg"":1},""precipitation"":{""sg"":1},""windDirection"":{""sg"":1},""windSpeed"":{""sg"":1}}]}");

            AWeatherForecast testedWeather = new StormglassWeatherForecast.StormglassWeatherForecast();
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
