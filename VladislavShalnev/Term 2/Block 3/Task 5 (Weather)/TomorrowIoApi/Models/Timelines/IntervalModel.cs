using WeatherApi;

namespace TomorrowIoApi.Models.Timelines;

public record IntervalModel : IWeatherModel
{
	public DateTime? StartTime { get; set; }
	public ValuesModel? Values { get; set; }

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
		WindSpeed = Values?.WindSpeed
	};

}