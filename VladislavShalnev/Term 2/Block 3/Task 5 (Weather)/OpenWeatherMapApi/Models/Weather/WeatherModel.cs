namespace OpenWeatherMapApi.Models.Weather;

public record WeatherModel
{
	public int? Id { get; init; }
	public string? Main { get; init; }
	public string? Description { get; init; }
	public string? Icon { get; init; }
}