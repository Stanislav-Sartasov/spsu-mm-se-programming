using Newtonsoft.Json;

namespace OpenWeatherMapApi.Models.Weather;

public record MainModel
{
	public double? Temp { get; init; }
	[JsonProperty("feels_like")]
	public double? FeelsLike { get; init; }
	[JsonProperty("temp_min")]
	public double? TempMin { get; init; }
	[JsonProperty("temp_max")]
	public double? TempMax { get; init; }
	public int? Pressure { get; init; }
	public int? Humidity { get; init; }
}