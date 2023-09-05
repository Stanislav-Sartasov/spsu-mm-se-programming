global using NUnit.Framework;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Task6.Network;
using static System.Net.Mime.MediaTypeNames;

namespace Task6Test
{
    public class ConnectionTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPublicVal()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Socket s1 = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp); // Close â Connection
            Socket l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);
            s1.Connect(localEndPoint);

            Connection c = new Connection(s1);
            Assert.AreEqual(c.LocalEndPoint, s1.LocalEndPoint);
            Assert.AreEqual(c.LocalIP, ipAddr);
            Assert.AreEqual(c.RemoteEndPoint, s1.RemoteEndPoint);
            Assert.AreEqual(c.RemoteIP, ipAddr);
            Assert.AreEqual(c.RemotePort, 5000);

            c.Close();
            l.Close();
        }

        [Test]
        public void TestConstructor()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Socket l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);

            Connection c = new Connection(ipAddr, 5000);
            Assert.AreEqual(c.RemoteIP, ipAddr);
            Assert.AreEqual(c.RemotePort, 5000);

            c.Close();
            l.Close();
        }

        [Test]
        public void TestSend()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Socket l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);

            Connection c = new Connection(ipAddr, 5000);
            string sendedText = String.Empty;
            c.Send(sendedText);
            Socket s = l.Accept(); // Close â Connection

            byte[] message = new byte[1024];
            int cntByte = s.Receive(message);
            string receivedText = Encoding.ASCII.GetString(message, 0, cntByte);

            Assert.AreEqual(sendedText + "\n", receivedText);

            c.Close();
            s.Close();
            l.Close();
        }

        [Test]
        public void TestSendReceive()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Socket l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);

            Connection c1 = new Connection(ipAddr, 5000);
            string sendedText = String.Empty;
            c1.Send(sendedText);
            Socket s = l.Accept(); // Close â Connection

            Connection c2 = new Connection(s);
            string receivedText = c2.Receive();

            Assert.AreEqual(sendedText + "\n", receivedText);

            c1.Close();
            c2.Close();
            l.Close();
        }

        [Test]
        public void TestNotConnected()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 5000);
            Connection c = new Connection(ipAddr, 5000);

            Assert.AreEqual(0, c.Send("sd"));
            Assert.AreEqual(String.Empty, c.Receive());
            c.Close();

            Socket l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);
            l.Close();
            c = new Connection(ipAddr, 5000);
            Assert.AreEqual(0, c.Send("sd"));
            c.Close();

            l = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            l.Bind(localEndPoint);
            l.Listen(100);
            l.Close();
            c = new Connection(ipAddr, 5000);
            Assert.AreEqual(String.Empty, c.Receive());
            c.Close();
        }
    }
}