namespace TomorrowIoApi.Exceptions;

public class ApiException : Exception
{
	public int Code { get; }
	public string Type { get; }

	public ApiException(int code, string type, string message) : base($"Code: {code}. {type}. " + message)
	{
		Code = code;
		Type = type;
	}
}