using NUnit.Framework;
using P2P.Utils;

namespace P2PUnitTests
{
    public class UtilsTests
    {
        [Test]
        public void GetPossitiveIntTest()
        {
            Assert.AreEqual(1, Utils.GetPositiveInt("1"));
            Assert.AreEqual(0, Utils.GetPositiveInt("0"));
            Assert.AreEqual(0, Utils.GetPositiveInt("-1"));
            Assert.AreEqual(0, Utils.GetPositiveInt("odin"));
        }
    }
}