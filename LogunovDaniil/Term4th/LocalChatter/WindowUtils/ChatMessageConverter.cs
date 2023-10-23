namespace LocalChatter.WindowUtils
{
	public static class ChatMessageConverter
	{
		public static string ConvertToReadableString(ChatMessage message)
		{
			var readableUsername = $"User ({message.Username})";
			switch (message.Type)
			{
				case MessageType.Message:
					return $"{readableUsername} :: {message.Content}";
				case MessageType.NewClientJoined:
					return $"{readableUsername} just joined!";
				case MessageType.ConnectConfirmation:
					return $"{readableUsername} was present before you joined";
				case MessageType.Connect:
					return $"Connected to {readableUsername}!";
				case MessageType.Leave:
					return $"{readableUsername} decided to leave for now!";
				default: // unreachable
					throw new ArgumentException("Unexpected MessageType in ChatMessageConverter!");
			}
		}
	}
}
