using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherIoC
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
			return "Source: " + RenameNullData(Source) + Environment.NewLine +
				"Temp (F): " + RenameNullData(TempFahrenheit) + Environment.NewLine +
				"Temp (C): " + RenameNullData(TempCelsius) + Environment.NewLine +
				"Cloud coverage(%): " + RenameNullData(CloudCoverage) + Environment.NewLine +
				"Humidity: " + RenameNullData(Humidity) + Environment.NewLine +
				"Precipitation: " + RenameNullData(Downfall) + Environment.NewLine +
				"Wind Direction: " + RenameNullData(WindDirection) + Environment.NewLine +
				"Wind Speed: " + RenameNullData(WindSpeed);
		}

		private string RenameNullData(string? data)
		{
			if (data != null)
				return data;
			else
				return "No Data";
		}
	}
}
