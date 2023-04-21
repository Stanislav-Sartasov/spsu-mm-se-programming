using WeatherApi;

namespace TomorrowIoApi.Models.Timelines;

public record IntervalModel : IWeatherModel
{
	public DateTime? StartTime { get; init; }
	public ValuesModel? Values { get; init; }

	public static explicit operator Weather(IntervalModel weather) =>
		weather.ToWeather();
	
	public Weather ToWeather() => new()
	{
		Date = StartTime,
		Temperature = Values?.Temperature,
		Humidity = Values?.Humidity,
		CloudCover = Values?.CloudCover,
		Precipitations = Values?.PrecipitationIntensity,
		WindDirection = Values?.WindDirection,
		WindSpeed = Values?.WindSpeed,
		FeelsLike = Values?.TemperatureApparent,
		Type = Values?.WeatherCode switch
		{
			1000 or 1100 => WeatherType.Clear,
			1101 or 1102 or 1001 => WeatherType.Cloudy,
			2000 or 2100 => WeatherType.Fog,
			4000 or 6000 => WeatherType.Drizzle,
			4001 or 4200 or 4201 or 6001 or 6200 or 6201 => WeatherType.Rain,
			5000 or 5001 or 5100 or 5101 or 7000 or 7101 or 7102 => WeatherType.Snow,
			8000 => WeatherType.Thunderstorm,
			_ => null
		},
		Description = GetDescription(Values?.WeatherCode)
	};

	private string? GetDescription(int? code) =>
		code switch
		{
			1000 => "Ясно, Солнечно",
			1100 => "В основном ясно",
			1101 => "Небольшая облачность",
			1102 => "Значительная облачность",
			1001 => "Пасмурно",
			2000 => "Туман",
			2100 => "Небольшой туман",
			4000 => "Морось",
			4001 => "Дождь",
			4200 => "Небольшой дождь",
			4201 => "Сильный дождь",
			5000 => "Снег",
			5001 => "Снег",
			5100 => "Небольшой снег",
			5101 => "Сильный снег",
			6000 => "Морось",
			6001 => "Моросящий дождь",
			6200 => "Небольшой моросящий дождь",
			6201 => "Сильный моросящий дождь",
			7000 => "Град",
			7101 => "Сильный град",
			7102 => "Небольшой град",
			8000 => "Гроза",
			_ => null
		};

}