using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeatherApi;

namespace OpenWeatherMapApi.Models.Weather;

public record OpenWeatherModel : IWeatherModel
{
	public CoordModel? Coord { get; set; }
	public WeatherModel[]? Weather { get; set; }
	public string? Base { get; set; }
	public MainModel? Main { get; set; }
	public int? Visibility { get; set; }
	public WindModel? Wind { get; set; }
	public CloudsModel? Clouds { get; set; }
	public RainModel? Rain { get; set; }
	public SnowModel? Snow { get; set; }
	
	[JsonConverter(typeof(UnixDateTimeConverter))]
	public DateTime? Dt { get; set; }
	
	public SysModel? Sys { get; set; }
	public int? Timezone { get; set; }
	public int? Id { get; set; }
	public string? Name { get; set; }
	public int? Cod { get; set; }
	
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