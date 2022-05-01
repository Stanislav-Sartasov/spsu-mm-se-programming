namespace OpenWeatherMapApi.Exceptions;

public class ApiException : WeatherApi.Exceptions.ApiException
{
	public int Code { get; }

	public ApiException(int code, string message) : base($"Code: {code}. " + message)
	{
		Code = code;
	}
}