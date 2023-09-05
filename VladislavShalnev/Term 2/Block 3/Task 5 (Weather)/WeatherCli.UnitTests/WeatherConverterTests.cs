using System;
using NUnit.Framework;
using WeatherApi;

namespace WeatherCli.UnitTests;

public class WeatherConverterTests
{
	[Test]
	public void WeatherToStringTest()
	{
		var weather = new Weather()
		{
			Temperature = 12,
			Humidity = 75,
			CloudCover = 50,
			Precipitations = 0,
			WindDirection = 60,
			WindSpeed = 5,
			Description = "Cloudy"
		};

		string expected = "Дата: Нет данных\nОписание: Cloudy\nТемпература (°C): 12°C\nТемпература (°F): 53,6°F\nОблачность (%): 50%\nВлажность (%): 75%\nОсадки (мм): 0 мм\nНаправление ветра (°): 60°\nСкорость ветра (м/с): 5 м/с";

		string actual = WeatherConverter.WeatherToString(weather);

		Assert.AreEqual(expected, actual);
	}
}