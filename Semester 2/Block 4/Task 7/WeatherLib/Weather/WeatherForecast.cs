using System;
using WeatherLib.Parsers;

namespace WeatherLib.Weather
{
	public class WeatherForecast
	{
		public string Temperature { get; private set; }
		public string CloudCover { get; private set; }
		public string Humidity { get; private set; }
		public string Precipitation { get; private set; }
		public string WindSpeed { get; private set; }
		public string WindDirection { get; private set; }
		public bool IsForecastReceived { get; private set; }
		public WeatherServices SourceSercvice { get; private set; }

		public WeatherForecast(bool isForecastReceived)
		{
			this.IsForecastReceived = isForecastReceived;
		}

		public WeatherForecast(
			string temperature,
			string cloudCover,
			string humidity,
			string precipitation,
			string windSpeed,
			string windDirection,
			WeatherServices sourceService)
		{
			Temperature = temperature;
			CloudCover = cloudCover;
			Humidity = humidity;
			Precipitation = precipitation;
			WindSpeed = windSpeed;
			WindDirection = windDirection;
			IsForecastReceived = true;
			SourceSercvice = sourceService;
		}

		public void Print()
		{
			if (IsForecastReceived == false)
			{
				Console.WriteLine("Something went wrong during receiving a response from web service.");
				return;
			}
			
			if (Temperature != null)
				Console.WriteLine($"Air temperature: {Temperature}\u00B0С, {1.8 * Convert.ToDouble(Temperature) + 32}\u00B0F");
			else
				Console.WriteLine("Air temperature: no data on this service");

			if (CloudCover != null)
				Console.WriteLine($"Cloud cover: {CloudCover}%");
			else
				Console.WriteLine("Cloud cover: no data on this service");

			if (Humidity != null)
				Console.WriteLine($"humidity: {Humidity}%");
			else
				Console.WriteLine("humidity: no data on this service");

			if (Precipitation != null)
				Console.WriteLine($"precipitation: {Precipitation}mm");
			else
				Console.WriteLine("precipitation: no data on this service");

			if (WindSpeed != null)
				Console.WriteLine($"windSpeed: {WindSpeed}m/s");
			else
				Console.WriteLine("windSpeed: no data on this service");

			if (WindDirection != null)
				Console.WriteLine($"windDirection: {WindDirection}\u00B0");
			else
				Console.WriteLine("windDirection: no data on this service");
		}

		public void PrepareForUI()
		{
			if (Temperature != null)
				Temperature += "°C";
			else
				Temperature = "no data";

			if (CloudCover != null)
				CloudCover += "%";
			else
				CloudCover = "no data";

			if (Humidity != null)
				Humidity += "%";
			else
				Humidity = "no data";

			if (Precipitation != null)
				Precipitation += "mm";
			else
				Precipitation = "no data";

			if (WindSpeed != null)
				WindSpeed += "m/s";
			else
				WindSpeed = "no data";

			if (WindDirection != null)
				WindDirection += "°";
			else
				WindDirection = "no data";
		}
	}
}
