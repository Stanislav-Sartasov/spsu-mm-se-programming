namespace LocalChatter
{
	public class ChatMessage
	{
		public string Username;
		public MessageType Type;
		public string Content;

		public ChatMessage(string name, MessageType type)
		{
			Username = name;
			Type = type;
			Content = string.Empty;
		}

		public ChatMessage(string name, MessageType type, string content)
		{
			Username = name;
			Type = type;
			Content = content;
		}

		public string Serialize()
		{
			string type;
			switch (Type)
			{
				case MessageType.Message:
					type = "M";
					break;
				case MessageType.Connect:
					type = "C";
					break;
				case MessageType.ConnectConfirmation:
					type = "+";
					break;
				case MessageType.Leave:
					type = "L";
					break;
				case MessageType.NewClientJoined:
					type = "J";
					break;
				default: // unreachable
					throw new ArgumentException("Unexpected MessageType value!");
			}
			var parts = new List<string>();
			parts.Add(type);
			parts.Add(Username);
			if (Content.Length > 0)
				parts.Add(Content);
			return String.Join(Config.Delimeter, parts.ToArray());
		}

		public static ChatMessage? DeserializeMessage(string message)
		{
			var splitMessage = message.Split(Config.Delimeter);

			string type = splitMessage[0];
			string name = splitMessage[1];
			string content;
			if (splitMessage.Length > 2)
				content = String.Join(Config.Delimeter, new ArraySegment<string>(splitMessage, 2, splitMessage.Length - 2));
			else
				content = string.Empty;

			ChatMessage chatMessage;
			switch (type)
			{
				case "M":
					chatMessage = new ChatMessage(name, MessageType.Message, content);
					break;
				case "C":
					chatMessage = new ChatMessage(name, MessageType.Connect);
					break;
				case "+":
					chatMessage = new ChatMessage(name, MessageType.ConnectConfirmation);
					break;
				case "L":
					chatMessage = new ChatMessage(name, MessageType.Leave);
					break;
				case "J":
					chatMessage = new ChatMessage(name, MessageType.NewClientJoined);
					break;
				default:
					return null;
			}
			return chatMessage;
		}
	}
}
