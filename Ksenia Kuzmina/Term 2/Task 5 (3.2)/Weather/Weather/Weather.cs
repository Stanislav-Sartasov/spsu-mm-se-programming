using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
	public class Weather
	{
		public readonly float? CelsiusTemperature;
		public readonly float? FahrenheitTemperature;
		public readonly string? CloudCover;
		public readonly int? Humidity;
		public readonly string? Precipitation;
		public readonly string? WindDirection;
		public readonly float? WindSpeed;

		public Weather(
			float? celsiusTemperature,
			float? fahrenheitTemperature,
			string? cloudCover,
			int? humidity,
			string? precipitation,
			string? windDirection,
			float? windSpeed)
		{
			CelsiusTemperature = celsiusTemperature;
			FahrenheitTemperature = fahrenheitTemperature;
			CloudCover = cloudCover;
			Humidity = humidity;
			Precipitation = precipitation;
			WindDirection = FindOutWindDirection(windDirection);
			WindSpeed = windSpeed;
		}

		private string FindOutWindDirection(string windDirection)
		{
			if (windDirection == null)
				return null;

			double deg = Convert.ToDouble(windDirection, CultureInfo.InvariantCulture);

			if ((deg >= 0) && (deg <= 90))
			{
				return "NE";
			}
			else if ((deg > 90) && (deg <= 180))
			{
				return "SE";
			}
			else if ((deg > 180) && (deg <= 270))
			{
				return "SW";
			}
			else
			{
				return "NW";
			}
		}
	}
}
