using P2PChat.Core.Json;

namespace P2PChat.Core.UnitTests.SocketTests;

public class StringMessage : IJsonObject<StringMessage>
{
	public string Field;

	public StringMessage(string field)
	{
		Field = field;
	}

	public static StringMessage FromJson(string json)
	{
		return new StringMessage(json);
	}

	public string ToJson()
	{
		return Field;
	}
}