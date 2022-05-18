using NUnit.Framework;

namespace RequestManagement.UnitTests
{
    public class APIRequestTests
    {
        [Test]
        public void ConstructorTest()
        {
            string url = "https://www.google.com";
            IRequest request = new APIRequest(url);

            Assert.IsNotNull(request);
        }

        [Test]
        public void SuccessfulGetTest()
        {
            string url = "https://www.google.com";
            var request = new APIRequest(url);
            request.Get();

            Assert.IsTrue(request.Connected);
            Assert.IsNotNull(request.Response);
        }

        [Test]
        public void InvalidLinkGetTest()
        {
            string url = "No valid link here";
            var request = new APIRequest(url);
            var expectedResponse = "Invalid link";
            request.Get();

            Assert.IsFalse(request.Connected);
            Assert.IsNotNull(request.Response);
            Assert.AreEqual(request.Response, expectedResponse);
        }

        [Test]
        public void UnsuccessfulGetTest()
        {
            string url = "https://api.tomorrow.io/v4/timelines";
            var request = new APIRequest(url);
            var expectedResponse = "Site is down";
            request.Get();

            Assert.IsFalse(request.Connected);
            Assert.IsNotNull(request.Response);
            Assert.AreEqual(request.Response, expectedResponse);
        }
    }
}