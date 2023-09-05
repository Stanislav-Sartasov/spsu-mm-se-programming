using P2PChat.Core.Json;

namespace P2PChat.Core.Message.Fallback;

/// <summary>
///		Response with listener port
/// </summary>
public class MyPortResponse : AJsonObject<MyPortResponse>
{
	public MyPortResponse(int port)
	{
		Port = port;
	}

	public int Port { get; }
}