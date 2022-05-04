using Newtonsoft.Json;

namespace OpenWeatherMapApi.Models.Weather;

public record RainModel
{
	[JsonProperty("1h")]
	public double? OneHour { get; init; }
	[JsonProperty("3h")]
	public double? ThreeHours { get; init; }
}