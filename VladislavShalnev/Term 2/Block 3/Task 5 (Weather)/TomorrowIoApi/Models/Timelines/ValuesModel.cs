namespace TomorrowIoApi.Models.Timelines;

public record ValuesModel
{
	public int? CloudCover { get; init; }
	public int? Humidity { get; init; }
	public int? PrecipitationIntensity { get; init; }
	public double? Temperature { get; init; }
	public double? WindDirection { get; init; }
	public double? WindSpeed { get; init; }
}