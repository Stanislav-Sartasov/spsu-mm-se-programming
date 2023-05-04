using System.Net;
using P2PChat.Core.Log;

namespace P2PChat.Core.UnitTests.AlgorithmTests;

public class FallbackTests
{
	private ILogger _logger;

	[SetUp]
	public void Setup()
	{
		_logger = new ConsoleLogger();
	}


	[Test]
	public void OnlyRootTest()
	{
		using var rootChat = new Chat.Chat(_logger);
		Assert.IsFalse(rootChat.Send("hi!"));
	}

	[Test]
	public void TwoChats()
	{
		/*
		 *     R
		 *    /
		 *   c
		 */

		using var rootChat = new Chat.Chat(_logger);
		using var childChat = new Chat.Chat(_logger);
		rootChat.Start();
		childChat.Start();
		childChat.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));

		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { rootChat, childChat }));
	}

	[Test]
	public void TwoChildren()
	{
		/*
		 *     R
		 *    / \
		 *   l   r
		 */

		using var rootChat = new Chat.Chat(_logger);
		using var childChatLeft = new Chat.Chat(_logger);
		using var childChatRight = new Chat.Chat(_logger);

		rootChat.Start();
		childChatLeft.Start();
		childChatRight.Start();

		childChatLeft.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatRight.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));

		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { rootChat, childChatLeft, childChatRight }));
	}

	[Test]
	public void TwoChildrenRootX()
	{
		/*
		 *     R (X)
		 *    / \
		 *   l   r
		 */

		using var rootChat = new Chat.Chat(_logger);
		using var childChatLeft = new Chat.Chat(_logger);
		using var childChatRight = new Chat.Chat(_logger);

		rootChat.Start();
		childChatLeft.Start();
		childChatRight.Start();

		Thread.Sleep(50);
		childChatLeft.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatRight.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));

		// Waiting to get fallback
		Thread.Sleep(50);

		rootChat.Stop();

		// Waiting to rebuild the network
		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { childChatLeft, childChatRight }));
	}

	[Test]
	public void ChildChildXRoot()
	{
		/*
		 *       R
		 *      /
		 *     l (X)
		 *    /
		 *   r
		 */

		using var rootChat = new Chat.Chat(_logger);
		using var childChatLeft = new Chat.Chat(_logger);
		using var childChatRight = new Chat.Chat(_logger);

		rootChat.Start();
		childChatLeft.Start();
		childChatRight.Start();

		childChatLeft.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatRight.Join(IPEndPoint.Parse($"127.0.0.1:{childChatLeft.Port}"));
		Thread.Sleep(50);

		// Removing left chat
		/*
		 *     R
		 *    /
		 *   r
		 */

		// Waiting to get fallback
		Thread.Sleep(50);
		childChatLeft.Stop();

		// Waiting to rebuild the network
		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { rootChat, childChatRight }));
	}

	[Test]
	public void ChildChildXChildXRoot()
	{
		/*
		 *       R
		 *      /
		 *     l (X)
		 *    /
		 *   r (X)
		 *    \
		 *     u
		 */

		using var rootChat = new Chat.Chat(_logger);
		using var childChatLeft = new Chat.Chat(_logger);
		using var childChatRight = new Chat.Chat(_logger);
		using var childChatUp = new Chat.Chat(_logger);

		rootChat.Start();
		childChatLeft.Start();
		childChatRight.Start();
		childChatUp.Start();

		childChatLeft.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatRight.Join(IPEndPoint.Parse($"127.0.0.1:{childChatLeft.Port}"));
		Thread.Sleep(50);
		childChatUp.Join(IPEndPoint.Parse($"127.0.0.1:{childChatRight.Port}"));
		Thread.Sleep(50);

		// Waiting to get fallback
		Thread.Sleep(50);
		childChatLeft.Stop();

		// Waiting to get fallback again
		Thread.Sleep(50);
		childChatRight.Stop();

		// Waiting to rebuild the network
		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { rootChat, childChatUp }));
	}

	[Test]
	public void ChildRootXChildXChild()
	{
		/*
		 *     R (X)
		 *    / \
		 *   l   r (X)
		 *        \
		 *         u
		 */

		using var rootChat = new Chat.Chat(new VoidLogger());
		using var childChatLeft = new Chat.Chat(_logger);
		using var childChatRight = new Chat.Chat(new VoidLogger());
		using var childChatUp = new Chat.Chat(_logger);

		rootChat.Start();
		childChatLeft.Start();
		childChatRight.Start();
		childChatUp.Start();

		childChatLeft.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatRight.Join(IPEndPoint.Parse($"127.0.0.1:{rootChat.Port}"));
		Thread.Sleep(50);
		childChatUp.Join(IPEndPoint.Parse($"127.0.0.1:{childChatRight.Port}"));
		Thread.Sleep(50);

		// Removing Root
		/*
		 *   l -> r
		 */

		// Waiting to get fallback
		Thread.Sleep(50);
		rootChat.Stop();

		// Waiting to get fallback
		Thread.Sleep(50);
		childChatRight.Stop();

		// Waiting to rebuild the network
		Thread.Sleep(50);

		Assert.IsTrue(AllChatsGetMessage(new List<Chat.Chat> { childChatLeft, childChatUp }));
	}


	// Returns if all chats are connected
	private static bool AllChatsGetMessage(List<Chat.Chat> chats)
	{
		foreach (var chat in chats)
		{
			chat.Send("Hi!");
			Thread.Sleep(50);
		}

		var mCount = chats.Sum(chat => chat.GetHistory().Count);

		return mCount == chats.Count * chats.Count;
	}
}