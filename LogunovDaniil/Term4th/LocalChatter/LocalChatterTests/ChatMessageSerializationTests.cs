using LocalChatter;

namespace LocalChatterTests
{
	public class Tests
	{
		private static readonly string NameJoe = "Joe";
		private static readonly string MessageFromJoe = "Hello from Joe!";

		private static readonly ChatMessage NormalMessage = new ChatMessage(NameJoe, MessageType.Message, MessageFromJoe);
		private static readonly string NormalMessageSerialized = $"M|{NameJoe}|{MessageFromJoe}";

		private static readonly ChatMessage JoeJoined = new ChatMessage(NameJoe, MessageType.NewClientJoined);
		private static readonly string JoeJoinedSerialized = $"J|{NameJoe}";

		[Test]
		public void NormalMessageSerializationTest()
		{
			Assert.That(NormalMessage.Serialize(), Is.EqualTo(NormalMessageSerialized));
		}

		[Test]
		public void JoeJoinedSerializationTest()
		{
			Assert.That(JoeJoined.Serialize(), Is.EqualTo(JoeJoinedSerialized));
		}

		[Test]
		public void NormalMessageDeserializationTest()
		{
			var deserialized = ChatMessage.DeserializeMessage(NormalMessageSerialized);
			Assert.IsNotNull(deserialized);
			Assert.That(deserialized.Username, Is.EqualTo(NameJoe));
			Assert.That(deserialized.Type, Is.EqualTo(MessageType.Message));
			Assert.That(deserialized.Content, Is.EqualTo(MessageFromJoe));
		}

		[Test]
		public void JoeJoinedDeserializationTest()
		{
			var deserialized = ChatMessage.DeserializeMessage(JoeJoinedSerialized);
			Assert.IsNotNull(deserialized);
			Assert.That(deserialized.Username, Is.EqualTo(NameJoe));
			Assert.That(deserialized.Type, Is.EqualTo(MessageType.NewClientJoined));
		}

		[Test]
		public void NormalMessageConverterTest()
		{
			var converted = LocalChatter.WindowUtils.ChatMessageConverter.ConvertToReadableString(NormalMessage);
			Assert.That(converted, Is.EqualTo($"User ({NameJoe}) :: {MessageFromJoe}"));
		}

		[Test]
		public void JoeJoinedConverterTest()
		{
			var converted = LocalChatter.WindowUtils.ChatMessageConverter.ConvertToReadableString(JoeJoined);
			Assert.That(converted, Is.EqualTo($"User ({NameJoe}) just joined!"));
		}

		[Test]
		public void MessageTypeDeserializationTest()
		{
			foreach (var type in Enum.GetValues(typeof(MessageType)).Cast<MessageType>())
			{
				var check = new ChatMessage(NameJoe, type).Serialize();
				var deserialized = ChatMessage.DeserializeMessage(check);
				Assert.IsNotNull(deserialized);
				Assert.That(type, Is.EqualTo(deserialized.Type));
			}
		}
	}
}