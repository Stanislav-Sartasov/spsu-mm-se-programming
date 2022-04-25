namespace WeatherApi;

public record Weather
{
	public double? Temperature { get; set; }
	public int? Humidity { get; set; }
	public int? CloudCover { get; set; }
	public double? Precipitations { get; set; }
	public double? WindDirection { get; set; }
	public double? WindSpeed { get; set; }
	public string? Description { get; set; }
	public DateTime? Date { get; set; }
}