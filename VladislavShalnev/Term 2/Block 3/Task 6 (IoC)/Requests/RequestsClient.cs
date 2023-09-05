namespace Requests;

public class RequestsClient : IRequestsClient
{
	private readonly HttpClient _httpClient;

	public RequestsClient()
	{
		_httpClient = new HttpClient();
		_httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
	}

	public async Task<string> GetStringAsync(string url)
	{
		using HttpResponseMessage response = await _httpClient.GetAsync(url);
		return await response.Content.ReadAsStringAsync();
	}
}