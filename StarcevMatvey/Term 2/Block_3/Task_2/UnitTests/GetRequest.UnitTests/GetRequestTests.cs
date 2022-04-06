using NUnit.Framework;
using Requests;

namespace RequestTests
{
    public class GetRequestTests
    {
        [Test]
        public void RunTest()
        {
            GetRequest request = new GetRequest("nonexistentsite", "nonexistentaccept", "nonexistenthost", "nonexistentreferer");
            Assert.Catch(() => request.Run());
            request = new GetRequest(
                "https://osu.ppy.sh/",
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9",
                "osu.ppy.sh",
                "");

            request.Run();
            Assert.IsTrue(request.Connect);
            Assert.Pass();
        }
    }
}