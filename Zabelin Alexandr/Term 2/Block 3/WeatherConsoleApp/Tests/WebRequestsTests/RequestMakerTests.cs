using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebRequests;

namespace WebRequestsTests
{
    [TestClass]
    public class RequestMakerTests
    {
        [TestMethod]
        public void GetJSONInvalidLinkTest()
        {
            IRequestMaker requestMaker = new RequestMaker();

            string json = requestMaker.GetJSON("https://notExistingAtAllLink/reallyItDoesntExist");

            Assert.AreEqual(json, "");
        }
    }
}