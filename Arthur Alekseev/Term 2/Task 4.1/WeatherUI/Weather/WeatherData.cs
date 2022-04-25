using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUI.Weather
{
	public class WeatherData
	{
		public readonly string Source;

		public readonly string? TempFahrenheit;
		public readonly string? TempCelsius;
		public readonly string? CloudCoverage;
		public readonly string? Humidity;
		public readonly string? Downfall;
		public readonly string? WindDirection;
		public readonly string? WindSpeed;

		public WeatherData(string? tempFahrenheit, string? tempCelsius, string? cloudCoverage, string? humidity, string? downfall, string? windDirection, string? windSpeed, string source)
		{
			Source = source;
			TempFahrenheit = tempFahrenheit;
			TempCelsius = tempCelsius;
			CloudCoverage = cloudCoverage;
			Humidity = humidity;
			Downfall = downfall;
			WindDirection = windDirection;
			WindSpeed = windSpeed;
		}

		public bool IsNotEmpty()
		{
			if (TempFahrenheit != null || TempCelsius != null || CloudCoverage != null ||
				Downfall != null || Humidity != null || WindDirection != null || WindSpeed != null)
				return true;
			return false;
		}

		public override string ToString()
		{
			return "Source: " + renameNullData(Source) + "\r\n" +
				"Temp (F): " + renameNullData(TempFahrenheit) + "\r\n" +
				"Temp (C): " + renameNullData(TempCelsius) + "\r\n" +
				"Cloud coverage(%): " + renameNullData(CloudCoverage) + "\r\n" +
				"Humidity: " + renameNullData(Humidity) + "\r\n" +
				"Precipitation: " + renameNullData(Downfall) + "\r\n" +
				"Wind Direction: " + renameNullData(WindDirection) + "\r\n" +
				"Wind Speed: " + renameNullData(WindSpeed);
		}

		private string renameNullData(string? data)
		{
			if (data != null)
				return data;
			else
				return "No Data";
		}
	}
}
