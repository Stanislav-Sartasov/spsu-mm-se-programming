namespace OpenWeatherMapApi.Models.Weather;

public record WindModel
{
	public double? Speed { get; init; }
	public int? Deg { get; init; }
}