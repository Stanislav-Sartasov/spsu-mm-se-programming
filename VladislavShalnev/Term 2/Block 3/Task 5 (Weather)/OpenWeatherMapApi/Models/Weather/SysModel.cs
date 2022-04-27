namespace OpenWeatherMapApi.Models.Weather;

public record SysModel
{
	public int? Type { get; init; }
	public int? Id { get; init; }
	public double? Message { get; init; }
	public string? Country { get; init; }
	public int? Sunrise { get; init; }
	public int? Sunset { get; init; }
}