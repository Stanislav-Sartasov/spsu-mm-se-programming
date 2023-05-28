using NUnit.Framework;
using P2PChat;
using System.Net;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        const string serverIp = "127.0.0.1";
        const int serverPort = 8000;

        [Test]
        public void ServerTest()
        {
            var server = new Server(serverIp, serverPort);

            server.Activate(DoNothing);
            Thread.Sleep(500);
            server.Dispose();

            Assert.Pass();
        }

        public void DoNothing()
        {
            Thread.Sleep(500);
        }

        [Test]
        public void PeerTest()
        {
            var peer = new Peer(serverIp, serverPort);

            peer.Activate();
            Thread.Sleep(500);
            peer.Dispose();

            Assert.Pass();
        }

        [Test]
        public void FullTest()
        {
            var p1 = new Peer(serverIp, serverPort);
            p1.Activate();

            var p2 = new Peer(serverIp, serverPort + 1);
            p2.Activate();

            p1.Connect(IPEndPoint.Parse($"{serverIp}:{serverPort + 1}"));

            p2.Send("Hello world!");

            p1.Dispose();
            p2.Dispose();

            Assert.Pass();
        }
    }
}