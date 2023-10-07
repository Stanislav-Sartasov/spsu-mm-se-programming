using LocalChatter;

namespace LocalChatterTests
{
	public class ChatClientTests
	{
		private static readonly string Machine1 = "127.0.0.1:4323";
		private static readonly string Machine2 = "127.0.0.1:4324";

		[Test]
		public void MessageForwardingTest()
		{
			ChatMessage receivedMessage = new ChatMessage("", MessageType.Message);
			string sent = "Hello there general Kenobi";

			var client = new ChatClient(Machine1, new Action<ChatMessage>((ChatMessage message) =>
			{
				receivedMessage = message;
			}));

			client.Start();
			client.SendMessage(sent);
			client.Dispose();

			Assert.That(receivedMessage.Content, Is.EqualTo(sent));
		}

		[Test]
		public void ClientConnectionTest()
		{
			ChatMessage newConnectionMessage = new ChatMessage("", MessageType.Message);
			ChatMessage connectionConfirmedMessage = new ChatMessage("", MessageType.Message);

			var clientReceiver = new ChatClient(Machine1, new Action<ChatMessage>((ChatMessage message) =>
			{
				newConnectionMessage = message;
			}));

			var clientSender = new ChatClient(Machine2, new Action<ChatMessage>((ChatMessage message) =>
			{
				connectionConfirmedMessage = message;
			}));

			try
			{
				clientReceiver.Start();
				clientSender.Start();

				Assert.True(clientSender.ConnectToClient(Machine1));

				Assert.That(newConnectionMessage.Username, Is.EqualTo(Machine2));
				Assert.That(newConnectionMessage.Type, Is.EqualTo(MessageType.NewClientJoined));

				Assert.That(connectionConfirmedMessage.Username, Is.EqualTo(Machine1));
				Assert.That(connectionConfirmedMessage.Type, Is.EqualTo(MessageType.Connect));
			}
			finally
			{
				clientReceiver.Dispose();
				clientSender.Dispose();
			}
		}
	}
}
