using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace TomorrowIoApi.Models.Timelines.UnitTests;

public class TimelineModelTests
{
	[Test]
	public void DeserializationTest()
	{
		var expected = new TimelineModel
		{
			Timestep = "current",
			EndTime = new DateTime(0),
			StartTime = new DateTime(0),
			Intervals = null
		};

		string actualString =
			@"{""data"":{""timelines"":[{""timestep"":""current"",""endTime"":""0001-01-01T00:00:00Z"",""startTime"":""0001-01-01T00:00:00Z""}]}}";

		var actual = JObject.Parse(actualString)["data"]?["timelines"]?[0]?.ToObject<TimelineModel>();
		
		Assert.AreEqual(expected, actual);
	}
}