using P2PChat.Core.Json;

namespace P2PChat.Core.Message.UserMessage;

/// <summary>
///     Message for user message
/// </summary>
public class UserMessage : AJsonObject<UserMessage>
{
	public UserMessage(string content, string name)
	{
		Content = content;
		Name = name;
	}

	/// <summary>
	///     Content of the message
	/// </summary>
	public string Content { get; }


	/// <summary>
	///     Message Author
	/// </summary>
	public string Name { get; }
}