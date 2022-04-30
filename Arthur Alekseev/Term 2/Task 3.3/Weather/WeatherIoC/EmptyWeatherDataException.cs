namespace WeatherIoC
{
	public class EmptyWeatherDataException : Exception
	{
		public EmptyWeatherDataException(string message) : base(message) { }
	}
}
