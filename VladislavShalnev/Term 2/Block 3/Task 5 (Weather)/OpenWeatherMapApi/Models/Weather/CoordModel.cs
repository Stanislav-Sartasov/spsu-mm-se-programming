namespace OpenWeatherMapApi.Models.Weather;

public record CoordModel
{
	public double? Lon { get; init; }
	public double? Lat { get; init; }
}