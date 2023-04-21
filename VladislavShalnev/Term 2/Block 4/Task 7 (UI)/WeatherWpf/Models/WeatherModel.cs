using System;
using WeatherApi;

namespace WeatherWpf.Models;

public record WeatherModel
{
	public string? TemperatureC { get; }
	public string? TemperatureF { get; }
	public string? FeelsLike { get; }
	public string? Humidity { get; }
	public string? CloudCover { get; }
	public string? Precipitations { get; }
	public string? WindDirection { get; }
	public string? WindSpeed { get; }
	public string? Description { get; }
	public string? Date { get; }
	public string? Error { get; init; }
	public WeatherStatus? Status { get; init; }
	public WeatherType? Type { get; }

	public WeatherModel(Weather weather)
	{
		DateTime? localDate = weather.Date is DateTime date
			? TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local)
			: null;
		
		Date = localDate?.ToString("g");
		int? temperature = (int?) weather.Temperature;
		TemperatureC = temperature.ToString();
		TemperatureF = ((int?)(temperature * 1.8 + 32)).ToString();
		FeelsLike = ((int?)weather.FeelsLike).ToString();
		Humidity = weather.Humidity.ToString();
		CloudCover = weather.CloudCover.ToString();
		Precipitations = weather.Precipitations.ToString();
		WindDirection = DegreesToDirection(weather.WindDirection);
		WindSpeed = weather.WindSpeed.ToString();
		Description = weather.Description;
		Type = weather.Type;
		Status = WeatherStatus.Loaded;
	}
	
	public WeatherModel() { }

	private string? DegreesToDirection(double? degrees) =>
		Math.Round(degrees / 45 ?? -1) switch
		{
			0 => "С",
			1 => "СВ",
			2 => "В",
			3 => "ЮВ",
			4 => "Ю",
			5 => "ЮЗ",
			6 => "З",
			7 => "СЗ",
			_ => null
		};
}