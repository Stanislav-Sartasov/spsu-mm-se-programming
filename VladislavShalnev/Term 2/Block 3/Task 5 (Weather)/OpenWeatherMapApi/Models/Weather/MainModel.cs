using Newtonsoft.Json;

namespace OpenWeatherMapApi.Models.Weather;

public record MainModel
{
	public double? Temp { get; set; }
	[JsonProperty("feels_like")]
	public double? FeelsLike { get; set; }
	[JsonProperty("temp_min")]
	public double? TempMin { get; set; }
	[JsonProperty("temp_max")]
	public double? TempMax { get; set; }
	public int? Pressure { get; set; }
	public int? Humidity { get; set; }
}