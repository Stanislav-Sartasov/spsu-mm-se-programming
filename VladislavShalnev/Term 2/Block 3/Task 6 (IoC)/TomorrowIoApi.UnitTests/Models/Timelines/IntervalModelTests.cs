using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TomorrowIoApi.Models.Timelines.UnitTests;

public class IntervalModelTests
{
	[Test]
	public void ToWeatherTest()
	{
		var intervalModel = new IntervalModel
		{
			StartTime = new DateTime(10000),
			Values = new ValuesModel
			{
				CloudCover = 100,
				Humidity = 66,
				PrecipitationIntensity = 10,
				Temperature = 13,
				WindDirection = 93,
				WindSpeed = 5
			}
		};

		var expected = new WeatherApi.Weather
		{
			Date = new DateTime(10000),
			Temperature = 13,
			Humidity = 66,
			CloudCover = 100,
			Precipitations = 10,
			WindDirection = 93,
			WindSpeed = 5,
			Description = null
		};

		WeatherApi.Weather actual = intervalModel.ToWeather();
		
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void DeserializationTest()
	{
		var expected = new IntervalModel
		{
			StartTime = new DateTime(0),
			Values = new ValuesModel
			{
				CloudCover = 98,
				Humidity = 85,
				PrecipitationIntensity = 0,
				Temperature = 6.5,
				WindDirection = 289.38,
				WindSpeed = 1.69
			}
		};

		string actualString =
			@"{""startTime"":""0001-01-01T00:00:00Z"",""values"":{""cloudCover"":98,""humidity"":85,""precipitationIntensity"":0,""temperature"":6.5,""windDirection"":289.38,""windSpeed"":1.69}}";

		var actual = JObject.Parse(actualString).ToObject<IntervalModel>();
		
		Assert.AreEqual(expected, actual);
	}
}