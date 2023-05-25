using NUnit.Framework;
using P2P.Utils;
using System.Net;

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

        [Test]
        public void TryPort()
        {
            Assert.Null(Utils.TryPort($"funny_port_228"));

            var p = $"127.0.0.1:1111";
            var adr = IPEndPoint.Parse($"127.0.0.1:1111");
            Assert.AreEqual(adr, Utils.TryPort(p));
        }
    }
}