using NUnit.Framework;
using P2P.Chat;
using System.Net;

namespace P2PUnitTests
{
    public class MainUnitTests
    {
        [Test]
        public void MainUnitTest()
        {
            var (p1, p2) = (1, 2);
            var (c1, c2) = (
                new Client(p1, (string x) => { }, (string x, string y) => { }),
                new Client(p2, (string x) => { }, (string x, string y) => { })
                );

            c1.Conect(IPEndPoint.Parse($"127.0.0.1:{p2}"));

            Assert.Pass();

            c1.Send($"HI!");
            c2.Send($"Yep, hi-hi!");

            Assert.Pass();

            c1.Dispose();
            c2.Dispose();

            Assert.Pass();
        }

    }
}
