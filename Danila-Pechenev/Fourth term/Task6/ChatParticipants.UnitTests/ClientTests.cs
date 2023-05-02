using NUnit.Framework;
using System.Threading;

namespace ChatParticipants.UnitTests
{
    public class ClientTests
    {
        [Test]
        public void CreateAndDisposeClientTest()
        {
            var client = new Client("127.0.0.1", 4040, Mock);
            client.Start();
            Thread.Sleep(500);
            client.Dispose();

            Assert.Pass();
        }

        private void Mock(string message)
        {
        }
    }
}
