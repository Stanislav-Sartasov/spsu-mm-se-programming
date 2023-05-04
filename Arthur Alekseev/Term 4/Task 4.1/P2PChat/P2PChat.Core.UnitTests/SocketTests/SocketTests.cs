using System.Net;
using P2PChat.Core.Log;
using P2PChat.Core.UnitTests.SocketTests;

namespace P2PChat.Core.Networking;

public class SocketTests
{
	[Test]
	public void ListenerClient()
	{
		var listener = new ListenerSocket(20010, new VoidLogger());
		var client = new ClientSocket(new VoidLogger());

		new Thread(() =>
		{
			listener.Start();
			var echo = listener.Accept();
			echo?.Send(new StringMessage(echo.Receive()));
		}).Start();

		var exchangeSocket = client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20010));

		exchangeSocket.Send(new StringMessage("Hello!"));

		var received = exchangeSocket.Receive();

		Assert.AreEqual(received, "Hello!");
	}
}