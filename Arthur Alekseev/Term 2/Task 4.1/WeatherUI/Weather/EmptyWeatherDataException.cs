﻿namespace WeatherUI.Weather
{
	public class EmptyWeatherDataException : Exception
	{
		public EmptyWeatherDataException(string message) : base(message) { }
	}
}
