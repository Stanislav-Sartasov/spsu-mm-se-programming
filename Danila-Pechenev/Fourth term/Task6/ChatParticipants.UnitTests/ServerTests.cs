using NUnit.Framework;
using System.Threading;

namespace ChatParticipants.UnitTests
{
    public class ServerTests
    {
        [Test]
        public void CreateAndDisposeServerTest()
        {
            var server = new Server("127.0.0.1", 10000);
            server.Start();
            Thread.Sleep(500);
            server.Dispose();

            Assert.Pass();
        }
    }
}
