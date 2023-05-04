using System.Net;
using P2PChat.Core.Log;
using P2PChat.Core.Message.UserMessage;

namespace P2PChat.Core.UnitTests.AlgorithmTests;

public class NameChangeTests
{
	private bool _allCool = true;


	[Test]
	public void OneChatTest()
	{
		using var chat = new Chat.Chat(new VoidLogger());
		chat.Start();
		chat.OnMessage += ValidateName;
		chat.ChangeName("A");
		Thread.Sleep(300);
		chat.Send("A");
	}

	[Test]
	public void TwoChatTest()
	{
		_allCool = true;

		using var chatOne = new Chat.Chat(new VoidLogger());
		using var chatTwo = new Chat.Chat(new VoidLogger());
		chatOne.Start();
		chatTwo.Start();

		chatOne.Join(IPEndPoint.Parse($"127.0.0.1:{chatTwo.Port}"));

		chatOne.OnMessage += ValidateName;
		chatTwo.OnMessage += ValidateName;

		chatOne.ChangeName("A");
		chatTwo.ChangeName("B");

		Thread.Sleep(300);

		chatOne.Send("A");
		chatTwo.Send("B");

		Thread.Sleep(300);

		if (!_allCool)
			Assert.Fail();
	}

	[Test]
	public void TwoChatLateThirdTest()
	{
		using var chatOne = new Chat.Chat(new VoidLogger());
		using var chatTwo = new Chat.Chat(new VoidLogger());
		using var chatThree = new Chat.Chat(new VoidLogger());

		chatOne.Start();
		chatTwo.Start();
		chatThree.Start();

		chatOne.OnMessage += ValidateName;
		chatTwo.OnMessage += ValidateName;
		chatTwo.OnMessage += ValidateName;

		chatOne.ChangeName("A");
		chatTwo.ChangeName("B");

		chatOne.Join(IPEndPoint.Parse($"127.0.0.1:{chatTwo.Port}"));

		chatThree.ChangeName("C");

		chatThree.Join(IPEndPoint.Parse($"127.0.0.1:{chatTwo.Port}"));

		Thread.Sleep(300);

		chatOne.Send("A");
		chatTwo.Send("B");
		chatThree.Send("C");

		Thread.Sleep(300);

		if (!_allCool)
			Assert.Fail();
	}

	[Test]
	public void FourChatsTest()
	{
		using var chatOne = new Chat.Chat(new VoidLogger());
		using var chatTwo = new Chat.Chat(new VoidLogger());
		using var chatThree = new Chat.Chat(new VoidLogger());
		using var chatFour = new Chat.Chat(new VoidLogger());

		chatOne.Start();
		chatTwo.Start();
		chatThree.Start();
		chatFour.Start();
		Thread.Sleep(50);

		chatOne.OnMessage += ValidateName;
		chatTwo.OnMessage += ValidateName;
		chatThree.OnMessage += ValidateName;
		chatFour.OnMessage += ValidateName;

		chatOne.ChangeName("A");
		Thread.Sleep(50);
		chatTwo.ChangeName("B");
		Thread.Sleep(50);
		chatOne.Join(IPEndPoint.Parse($"127.0.0.1:{chatTwo.Port}"));
		Thread.Sleep(50);
		chatThree.ChangeName("C");
		Thread.Sleep(50);
		chatThree.Join(IPEndPoint.Parse($"127.0.0.1:{chatTwo.Port}"));
		Thread.Sleep(50);
		chatFour.Join(IPEndPoint.Parse($"127.0.0.1:{chatThree.Port}"));
		Thread.Sleep(50);
		chatFour.ChangeName("D");
		Thread.Sleep(50);
		chatOne.Send("A");
		Thread.Sleep(50);
		chatTwo.Send("B");
		Thread.Sleep(50);
		chatFour.Send("D");
		Thread.Sleep(50);

		if (!_allCool)
			Assert.Fail();
	}

	private void ValidateName(UserMessage message)
	{
		Console.WriteLine(message.Content + " " + message.Name);
		_allCool = message.Content == message.Name && _allCool;
	}
}