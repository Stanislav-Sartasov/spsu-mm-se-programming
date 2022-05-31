using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace OpenWeatherMapApi.Models.Weather.UnitTests;

public class OpenWeatherModelTests
{
	[Test]
	public void ToWeatherTest()
	{
		var openWeatherModel = new OpenWeatherModel
		{
			Dt = new DateTime(10000),
			Main = new MainModel
			{
				Temp = 13,
				Humidity = 99
			},
			Clouds = new CloudsModel
			{
				All = 60
			},
			Rain = new RainModel
			{
				OneHour = 1
			},
			Snow = new SnowModel
			{
				OneHour = 3
			},
			Wind = new WindModel
			{
				Deg = 23,
				Speed = 10
			},
			Weather = new []
			{
				new WeatherModel
				{
					Description = "cloudy"
				}
			}
		};

		var expected = new WeatherApi.Weather
		{
			Date = new DateTime(10000),
			Temperature = 13,
			Humidity = 99,
			CloudCover = 60,
			Precipitations = 4,
			WindDirection = 23,
			WindSpeed = 10,
			Description = "Cloudy"
		};

		WeatherApi.Weather actual = openWeatherModel.ToWeather();
		
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void DeserializationTest()
	{
		var expected = new OpenWeatherModel
		{
			Coord = new CoordModel
			{
				Lon = 30.3159,
				Lat = 59.9391
			},
			Base = "stations",
			Main = new MainModel
			{
				Temp = 5.1,
				FeelsLike = 1.46,
				TempMin = 4.99,
				TempMax = 5.5,
				Pressure = 1001,
				Humidity = 92
			},
			Visibility = 10000,
			Wind = new WindModel
			{
				Speed = 5,
				Deg = 310
			},
			Clouds = new CloudsModel
			{
				All = 100
			},
			Dt = new DateTime(0).AddYears(1969),
			Sys = new SysModel
			{
				Type = 2,
				Id = 197864,
				Country = "RU",
				Sunrise = 1650852832,
				Sunset = 1650908357
			},
			Timezone = 10800,
			Id = 519690,
			Name = "Новая Голландия",
			Rain = new RainModel
			{
				OneHour = 1,
				ThreeHours = 3
			},
			Snow = new SnowModel
			{
				OneHour = 3,
				ThreeHours = 4
			},
			Cod = 200
		};

		string actualString =
			@"{""coord"":{""lon"":30.3159,""lat"":59.9391},""base"":""stations"",""main"":{""temp"":5.1,""feels_like"":1.46,""temp_min"":4.99,""temp_max"":5.5,""pressure"":1001,""humidity"":92},""visibility"":10000,""wind"":{""speed"":5,""deg"":310},""clouds"":{""all"":100},""dt"":0,""sys"":{""type"":2,""id"":197864,""country"":""RU"",""sunrise"":1650852832,""sunset"":1650908357},""timezone"":10800,""id"":519690,""name"":""Новая Голландия"",""rain"":{""1h"":1,""3h"":3},""snow"":{""1h"":3,""3h"":4},""cod"":200}";

		var actual = JObject.Parse(actualString).ToObject<OpenWeatherModel>();
		
		Assert.AreEqual(expected, actual);
	}
}