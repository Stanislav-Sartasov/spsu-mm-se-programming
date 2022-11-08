using NUnit.Framework;
using Request;

namespace RequestTests
{
	public class GetRequestTests
	{
		[Test]
		public void RunTest()
		{
			GetRequest request = new("https://api.exmo.com/v1/currency");
			request.Run();
			Assert.IsNotNull(request.Response);

			Assert.Pass();
		}
	}
}