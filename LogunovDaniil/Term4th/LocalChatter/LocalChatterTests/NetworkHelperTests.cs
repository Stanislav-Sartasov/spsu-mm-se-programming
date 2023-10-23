using LocalChatter.Utils;

namespace LocalChatterTests
{
	public class NetworkHelperTests
	{
		[Test]
		public void NormalIPTest()
		{
			string ip = "167.192.0.1";
			string port = "228";
			var endPoint = NetworkHelper.GetEndPointFromName($"{ip}:{port}");
			Assert.IsNotNull(endPoint);
			Assert.That(endPoint.Address.ToString(), Is.EqualTo(ip));
			Assert.That(endPoint.Port.ToString(), Is.EqualTo(port));
		}

		[Test]
		public void RandomStringTest()
		{
			Assert.IsNull(NetworkHelper.GetEndPointFromName("but it was me, Dio!"));
		}
	}
}
