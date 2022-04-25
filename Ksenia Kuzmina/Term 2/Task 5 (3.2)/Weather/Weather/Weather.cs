using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
	public class Weather
	{
		public float? CelsiusTemperature;
		public float? FahrenheitTemperature;
		public string? CloudCover;
		public int? Humidity;
		public string? Precipitation;
		public string? WindDirection;
		public float? WindSpeed;

		public Weather SetWeather(
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
			_findOutPrecipation(precipitation);
			_findOutWindDirection(windDirection);
			WindSpeed = windSpeed;
			return this;
		}

		private void _findOutPrecipation(string precipation)
		{
			bool isNum = int.TryParse(precipation, out int num);
			if (isNum)
			{
				int prec = Int32.Parse(precipation);

				if (prec == 0)
					Precipitation = "No precipitation";
				else if (prec == 1)
					Precipitation = "Rain";
				else if (prec == 2)
					Precipitation = "Snow";
				else
					Precipitation = "Undefined";
			}
			else
				Precipitation = precipation;
		}

		private void _findOutWindDirection(string windDirection)
		{
			double deg = Convert.ToDouble(windDirection.Replace(".", ","));

			if ((deg >= 0) && (deg <= 90))
			{
				WindDirection = "NE";
			}
			else if ((deg > 90) && (deg <= 180))
			{
				WindDirection = "SE";
			}
			else if ((deg > 180) && (deg <= 270))
			{
				WindDirection = "SW";
			}
			else
			{
				WindDirection = "NW";
			}
		}
	}
}
