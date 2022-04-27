using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeatherApi;

namespace OpenWeatherMapApi.Models.Weather;

public record OpenWeatherModel : IWeatherModel
{
	public CoordModel? Coord { get; init; }
	public WeatherModel[]? Weather { get; init; }
	public string? Base { get; init; }
	public MainModel? Main { get; init; }
	public int? Visibility { get; init; }
	public WindModel? Wind { get; init; }
	public CloudsModel? Clouds { get; init; }
	public RainModel? Rain { get; init; }
	public SnowModel? Snow { get; init; }
	
	[JsonConverter(typeof(UnixDateTimeConverter))]
	public DateTime? Dt { get; init; }
	
	public SysModel? Sys { get; init; }
	public int? Timezone { get; init; }
	public int? Id { get; init; }
	public string? Name { get; init; }
	public int? Cod { get; init; }
	
	public static explicit operator WeatherApi.Weather(OpenWeatherModel weather) =>
		weather.ToWeather();
	
	public WeatherApi.Weather ToWeather() => new()
	{
		Date = Dt,
		Temperature = Main?.Temp,
		Humidity = Main?.Humidity,
		CloudCover = Clouds?.All,
		Precipitations = (Rain?.OneHour ?? 0) + (Snow?.OneHour ?? 0),
		WindDirection = Wind?.Deg,
		WindSpeed = Wind?.Speed,
		Description = Weather?[0].Description is not null
			? ToUpperFirstLetter(Weather?[0].Description!)
			: null
	};

	private string ToUpperFirstLetter(string str) =>
		str.Substring(0, 1).ToUpper() + str.Substring(1);
}