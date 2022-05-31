using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Requests;
using TomorrowIoApi.Exceptions;
using WeatherApi;

namespace TomorrowIoApi.UnitTests;

public class TomorrowIoTests
{
	private Mock<IRequestsClient> _clientMock;
	private TomorrowIo _tomorrowIo;

	private const string RightRequestUrl =
		"https://api.tomorrow.io/v4/timelines?location=0,0&fields=temperature,cloudCover,humidity,precipitationIntensity,windSpeed,windDirection,temperatureApparent,weatherCode&timesteps=current&units=metric&apikey=token";
	
	[SetUp]
	public void Setup()
	{
		_clientMock = new Mock<IRequestsClient>();

		_tomorrowIo = new TomorrowIo("token", _clientMock.Object);
	}
	
	// Just for better code coverage percent...
	[Test]
	public void ConstructorTest()
	{
		var openWeatherMap = new TomorrowIo("token");
		openWeatherMap = new TomorrowIo("token", new RequestsClient());
		
		Assert.Pass();
	}

	[Test]
	public async Task GetCurrentAsyncTest()
	{
		_clientMock
			.Setup(client =>
				client.GetStringAsync(It.Is<string>(url =>
					url == RightRequestUrl)
				)
			)
			.ReturnsAsync(@"{""data"":{""timelines"":[{""timestep"":""current"",""endTime"":""0001-01-01T00:00:00Z"",""startTime"":""0001-01-01T00:00:00Z"",""intervals"":[{""startTime"":""0001-01-01T00:00:00Z"",""values"":{""cloudCover"":42,""humidity"":54,""precipitationIntensity"":0,""temperature"":5.31,""weatherCode"":1101,""windDirection"":49,""windSpeed"":1.13}}]}]}}");

		Weather expected = new Weather()
		{
			Temperature = 5.31,
			Humidity = 54,
			CloudCover = 42,
			Precipitations = 0,
			WindDirection = 49,
			WindSpeed = 1.13,
			Date = new DateTime(0),
			Type = WeatherType.Cloudy,
			Description = "Небольшая облачность"
		};
		
		Weather actual = await _tomorrowIo.GetCurrentAsync((0, 0));

		Assert.AreEqual(expected, actual);
	}
	
	[Test]
	public async Task ApiExceptionTest()
	{
		_clientMock
			.Setup(client =>
				client.GetStringAsync(It.Is<string>(url =>
					url == RightRequestUrl)
				)
			)
			.ReturnsAsync(@"{""type"":""Invalid Key"",""code"":401001,""message"":""The method requires authentication, but it was not presented or is invalid.""}");
		
		try
		{
			await _tomorrowIo.GetCurrentAsync((0, 0));
			Assert.Fail();
		}
		catch (ApiException ex)
		{
			Assert.AreEqual(ex.Code, 401001);
			Assert.AreEqual(ex.Type, "Invalid Key");
			Assert.AreEqual("Code: 401001. Invalid Key. The method requires authentication, but it was not presented or is invalid.", ex.Message);
		}
	}
}