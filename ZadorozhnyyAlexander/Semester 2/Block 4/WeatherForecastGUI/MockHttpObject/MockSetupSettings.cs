using Moq;
using Moq.Language.Flow;

namespace MockHttpObject
{
    public class MockSetupSettings
    {
		private ISetup<HttpMessageHandler, Task<HttpResponseMessage>> setupSettings;

		public MockSetupSettings(ISetup<HttpMessageHandler, Task<HttpResponseMessage>> newSetup)
        {
			setupSettings = newSetup;
		}

		public void Respond(string response, System.Net.HttpStatusCode status = System.Net.HttpStatusCode.OK)
		{
			var responseMessage = new HttpResponseMessage();
			responseMessage.StatusCode = status;

			responseMessage.Content = new StringContent(response);

			setupSettings.ReturnsAsync(responseMessage);
		}
	}
}