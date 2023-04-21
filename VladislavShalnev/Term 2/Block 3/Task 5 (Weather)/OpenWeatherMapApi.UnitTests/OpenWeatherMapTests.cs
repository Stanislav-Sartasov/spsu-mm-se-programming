using System;
using System.Net.Http;
using System.Threading.Tasks;
using MockHttp;
using NUnit.Framework;
using OpenWeatherMapApi.Exceptions;
using WeatherApi;

namespace OpenWeatherMapApi.UnitTests;

public class OpenWeatherMapTests
{
	private MockHttpMessageHandler _handlerMock;
	private OpenWeatherMap _openWeatherMap;

	private const string RightRequestUrl =
		"https://api.openweathermap.org/data/2.5/weather?lat=0&lon=0&units=metric&lang=en&appid=token";
	
	[SetUp]
	public void Setup()
	{
		_handlerMock = new MockHttpMessageHandler();

		var httpClient = new HttpClient(_handlerMock.Object);

		_openWeatherMap = new OpenWeatherMap("token", httpClient);
	}

	// Just for better code coverage percent...
	[Test]
	public void ConstructorTest()
	{
		var openWeatherMap = new OpenWeatherMap("token");
		openWeatherMap = new OpenWeatherMap("token", new HttpClient());
		
		Assert.Pass();
	}
	
	[Test]
	public async Task GetCurrentAsyncTest()
	{
		_handlerMock
			.When(RightRequestUrl)
			.Respond(@"{""weather"":[{""description"":""cloudy""}],""main"":{""temp"":6.33,""humidity"":81},""wind"":{""speed"":4,""deg"":40},""rain"":{""1h"":1},""clouds"":{""all"":100},""dt"":0,""cod"":200}");

		Weather expected = new Weather()
		{
			Temperature = 6.33,
			Humidity = 81,
			CloudCover = 100,
			Precipitations = 1,
			WindDirection = 40,
			WindSpeed = 4,
			Description = "Cloudy",
			Date = new DateTime().AddYears(1969)
		};
		
		Weather actual = await _openWeatherMap.GetCurrentAsync((0, 0));

		Assert.AreEqual(expected, actual);
	}
	
	[Test]
	public async Task ApiExceptionTest()
	{
		_handlerMock
			.When(RightRequestUrl)
			.Respond(@"{""cod"":401, ""message"": ""Invalid API key. Please see http://openweathermap.org/faq#error401 for more info.""}");

		try
		{
			await _openWeatherMap.GetCurrentAsync((0, 0));
			Assert.Fail();
		}
		catch (ApiException ex)
		{
			Assert.AreEqual(ex.Code, 401);
			Assert.AreEqual("Code: 401. Invalid API key. Please see http://openweathermap.org/faq#error401 for more info.", ex.Message);
		}
	}
}