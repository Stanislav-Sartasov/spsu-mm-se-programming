namespace OpenWeatherMapApi.Models.Weather;

public record SysModel
{
	public int? Type { get; set; }
	public int? Id { get; set; }
	public double? Message { get; set; }
	public string? Country { get; set; }
	public int? Sunrise { get; set; }
	public int? Sunset { get; set; }
}