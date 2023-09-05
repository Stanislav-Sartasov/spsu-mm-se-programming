using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TomorrowIoApi.Models.Timelines.UnitTests;

public class IntervalModelTests
{
	[TestCase(1000, "Ясно, Солнечно")]
	[TestCase(1100, "В основном ясно")]
	[TestCase(1101, "Небольшая облачность")]
	[TestCase(1102, "Значительная облачность")]
	[TestCase(1001, "Пасмурно")]
	[TestCase(2000, "Туман")]
	[TestCase(2100, "Небольшой туман")]
	[TestCase(4000, "Морось")]
	[TestCase(4001, "Дождь")]
	[TestCase(4200, "Небольшой дождь")]
	[TestCase(4201, "Сильный дождь")]
	[TestCase(5000, "Снег")]
	[TestCase(5001, "Снег")]
	[TestCase(5100, "Небольшой снег")]
	[TestCase(5101, "Сильный снег")]
	[TestCase(6000, "Морось")]
	[TestCase(6001, "Моросящий дождь")]
	[TestCase(6200, "Небольшой моросящий дождь")]
	[TestCase(6201, "Сильный моросящий дождь")]
	[TestCase(7000, "Град")]
	[TestCase(7101, "Сильный град")]
	[TestCase(7102, "Небольшой град")]
	[TestCase(8000, "Гроза")]
	[TestCase(null, null)]
	public void ToWeatherTest(int? weatherCode, string? description)
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
				WindSpeed = 5,
				WeatherCode = weatherCode
			}
		};

		WeatherApi.Weather actual = intervalModel.ToWeather();
		
		var expected = new WeatherApi.Weather
		{
			Date = new DateTime(10000),
			Temperature = 13,
			Humidity = 66,
			CloudCover = 100,
			Precipitations = 10,
			WindDirection = 93,
			WindSpeed = 5,
			Description = description,
			Type = actual.Type
		};

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