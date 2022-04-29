namespace Requests;

public interface IRequestsClient
{
	public Task<string> GetStringAsync(string url);
}