using System.Net;
using System.Net.Sockets;
using System.Threading;
using NUnit.Framework;

namespace P2PChat.UnitTests;

public class ConnectionTests
{
	[Test]
	public void SendReceiveTest()
	{
		int port = 3000;

		var endPoint = new IPEndPoint(IPAddress.Any, port);

		var acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		acceptor.Bind(endPoint);
		acceptor.Listen();

		var acceptorThread = new Thread(() =>
		{
			var conn = new Connection(acceptor.Accept());
			var received = conn.Receive();
			conn.Send("Another test");
			Assert.AreEqual(received, "Test");
			conn.Close();
		});

		acceptorThread.Start();

		var senderThread = new Thread(() =>
		{
			var conn = new Connection(IPEndPoint.Parse($"127.0.0.1:{port}"));
			conn.Send("Test");
			var received = conn.Receive();
			Assert.AreEqual(received, "Another test");
			conn.Close();
		});

		senderThread.Start();

		Thread.Sleep(100);

		acceptorThread.Join();
		senderThread.Join();
	}
}