using NUnit.Framework;
using WeatherGetter;

namespace Task_7.UnitTests
{
	public class RequestTest
	{
		[Test]
		public void RunTest()
		{
			Request request = new Request();
			Assert.Catch(() => request.Run(" "));
			Assert.IsFalse(request.Connected);

			request.Run("https://github.com/ch3zych3z/spbu-se-mathematics");
			Assert.IsTrue(request.Connected);
			Assert.IsNotNull(request.Response);

			Assert.Pass();
		}
	}
}
