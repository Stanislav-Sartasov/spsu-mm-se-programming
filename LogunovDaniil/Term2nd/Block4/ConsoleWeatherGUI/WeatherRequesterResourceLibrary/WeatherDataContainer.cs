namespace WeatherRequesterResourceLibrary
{
	public class WeatherDataContainer
	{
		public string SourceName { get; init; } = "unnamed source";

		public double TempC { get; init; }
		public double TempF { get; init; }
		public int Humidity { get; init; }
		public double WindSpeed { get; init; }
		public WindDirections WindDirection { get; init; }
		public PrecipitationTypes Precipitation { get; init; }
		public double? PrecipitationProbability { get; init; }
		public double? PrecipitationVolumetric { get; init; }
		public int CloudCover { get; init; }
	}
}
