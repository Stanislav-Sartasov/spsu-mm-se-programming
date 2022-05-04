namespace WeatherApi;

public record Weather
{
	public double? Temperature { get; init; }
	public int? Humidity { get; init; }
	public int? CloudCover { get; init; }
	public double? Precipitations { get; init; }
	public double? WindDirection { get; init; }
	public double? WindSpeed { get; init; }
	public string? Description { get; init; }
	public DateTime? Date { get; init; }
}