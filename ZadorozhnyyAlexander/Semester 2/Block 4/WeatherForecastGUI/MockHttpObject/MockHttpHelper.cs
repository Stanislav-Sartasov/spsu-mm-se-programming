using Moq;
using Moq.Protected;

namespace MockHttpObject
{
    public class MockHttpHelper
    {
        private Mock<HttpMessageHandler> mockMessageHandler;

        public MockHttpHelper()
        {
            mockMessageHandler = new Mock<HttpMessageHandler>();
        }

        public HttpMessageHandler Object()
        {
            return mockMessageHandler.Object;
        }

        public MockSetupSettings When(string url)
        {
            return new(
            mockMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(request =>
                    request.Method == HttpMethod.Get && request.RequestUri == new Uri(url)),
                ItExpr.IsAny<CancellationToken>())
            );
        }
    }
}
