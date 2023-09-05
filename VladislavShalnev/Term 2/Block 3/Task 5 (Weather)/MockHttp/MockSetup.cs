using Moq;
using Moq.Language.Flow;

namespace MockHttp;

public class MockSetup
{
	private ISetup<HttpMessageHandler, Task<HttpResponseMessage>> _setup;

	public MockSetup(ISetup<HttpMessageHandler, Task<HttpResponseMessage>> setup) =>
		_setup = setup;

	public void Respond(string response)
	{
		_setup.ReturnsAsync(new HttpResponseMessage
		{
			Content = new StringContent(response)
		});
	}
}