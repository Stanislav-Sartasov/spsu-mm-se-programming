global using NUnit.Framework;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Task6.Network;
using static System.Net.Mime.MediaTypeNames;

namespace Task6Test
{
    public class ListenerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructor()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Listener l = new Listener(5000);

            Assert.AreEqual(l.LocalPort, 5000);
            Assert.AreEqual(l.LocalIP, ipAddr);
            Assert.AreEqual(l.LocalEndPoint, localEndPoint);

            l.Close();
        }

        [Test]
        public void TestAccept()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            Listener l = new Listener(5000);
            Connection c = new Connection(ipAddr, 5000);
            string sendedText = "Test";
            c.Send(sendedText);
            Connection cl = l.Accept();
            string receivedText = cl.Receive();
            Assert.AreEqual(sendedText + "\n", receivedText);

            cl.Send(sendedText + "2");
            receivedText = c.Receive();
            Assert.AreEqual(sendedText + "2\n", receivedText);

            c.Close();
            cl.Close();
            l.Close();
        }
    }
}