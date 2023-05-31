using System.Net;
using NUnit.Framework;

namespace P2PChat.UnitTests;

public class ChatTests
{
	private Chat _firstChat;
	private Chat _secondChat;
	private Chat _thirdChat;
	private int _firstPort = 3000;
	private int _secondPort = 4000;
	private int _thirdPort = 5000;

	[Test]
	public void ThreeChatTest()
	{
		_firstChat = new Chat(_firstPort);
		_secondChat = new Chat(_secondPort);
		_thirdChat = new Chat(_thirdPort);

		string first = "";
		string second = "";
		string third = "";

		var helper = (ChatEvent e) => e switch
		{
			ChatEvent.Message => "M",
			ChatEvent.Connect => "C",
			ChatEvent.Disconnect => "D",
			_ => "E"
		};

		_firstChat.OnEvent += (e, _, _) => first += helper(e);
		_secondChat.OnEvent += (e, _, _) => second += helper(e);
		_thirdChat.OnEvent += (e, _, _) => third += helper(e);

		_secondChat.Connect(IPEndPoint.Parse($"127.0.0.1:{_firstPort}"));

		_firstChat.Send("Test 1");
		_secondChat.Send("Test 2");

		_thirdChat.Connect(IPEndPoint.Parse($"127.0.0.1:{_secondPort}"));

		_thirdChat.Send("Test 3");

		_firstChat.Dispose();

		_secondChat.Send("Test 4");

		Assert.AreEqual("CMMCM", first);
		Assert.AreEqual("CMMCMDM", second);
		Assert.AreEqual("CMDM", third);

		_secondChat.Dispose();
		_thirdChat.Dispose();
	}
}