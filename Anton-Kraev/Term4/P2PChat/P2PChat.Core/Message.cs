using System.Net;
using System.Text.Json;

namespace P2PChat.Core;

public record Message(MessageType Type, IPEndPoint Sender, string? Text = null)
{
    public byte[] Serialize()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this);
    }

    public static Message? Deserialize(byte[] data)
    {
        return JsonSerializer.Deserialize<Message>(data);
    }

    public override string ToString()
    {
        return "";
    }
}