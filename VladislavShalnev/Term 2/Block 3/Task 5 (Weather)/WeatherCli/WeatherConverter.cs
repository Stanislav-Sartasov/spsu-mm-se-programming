using WeatherApi;

namespace WeatherCli;

public static class WeatherConverter
{
	private const string NoData = "Нет данных";
	
	public static string WeatherToString(Weather weather)
	{
		string result = $"Дата: {ValueToString(weather.Date)}\n";
		result += $"Описание: {ValueToString(weather.Description)}\n";
		result += $"Температура (°C): {ValueToString(weather.Temperature)}°C\n";
		result += $"Температура (°F): {ValueToString((weather.Temperature * 1.8 + 32 ?? 0).ToString("#.##"))}°F\n";
		result += $"Облачность (%): {ValueToString(weather.CloudCover)}%\n";
		result += $"Влажность (%): {ValueToString(weather.Humidity)}%\n";
		result += $"Осадки (мм): {ValueToString(weather.Precipitations)} мм\n";
		result += $"Направление ветра (°): {ValueToString(weather.WindDirection)}°\n";
		result += $"Скорость ветра (м/с): {ValueToString(weather.WindSpeed)} м/с";

		return result;
	}

	private static string ValueToString(object? value) =>
		value?.ToString() ?? NoData;
	
}