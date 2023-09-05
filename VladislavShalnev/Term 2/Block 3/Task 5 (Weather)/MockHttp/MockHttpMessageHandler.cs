using Moq;
using Moq.Protected;

namespace MockHttp;

// Mock wrappers for more convenient WeatherApi testing
public class MockHttpMessageHandler
{
	private Mock<HttpMessageHandler> _handlerMock;

	public HttpMessageHandler Object => _handlerMock.Object;

	public MockHttpMessageHandler() =>
		_handlerMock = new Mock<HttpMessageHandler>();

	public MockSetup When(string url) => new(
		_handlerMock
			.Protected()
			.Setup<Task<HttpResponseMessage>>(
				"SendAsync",
				ItExpr.Is<HttpRequestMessage>(request => 
					request.Method == HttpMethod.Get && request.RequestUri == new Uri(url)
				),
				ItExpr.IsAny<CancellationToken>()
			)
	);

}