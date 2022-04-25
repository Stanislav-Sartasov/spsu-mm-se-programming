namespace TomorrowIoApi.Models.Timelines;

public record ValuesModel
{
	public int? CloudCover { get; set; }
	public int? Humidity { get; set; }
	public int? PrecipitationIntensity { get; set; }
	public double? Temperature { get; set; }
	public double? WindDirection { get; set; }
	public double? WindSpeed { get; set; }
}