namespace OpenWeatherMapApi.Models.Weather;

public record WindModel
{
	public double? Speed { get; set; }
	public int? Deg { get; set; }
}