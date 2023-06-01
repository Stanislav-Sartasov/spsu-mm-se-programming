using System.Text;
using System.Text.Json;

namespace P2PChat.Core;

public record Message(MessageType Type, string Sender, string Text = "")
{
    public byte[] Serialize()
    {
        return JsonSerializer.SerializeToUtf8Bytes(this);
    }

    public static Message? Deserialize(byte[] data, int length)
    {
        var json = Encoding.Default.GetString(data, 0, length);
        return JsonSerializer.Deserialize<Message>(json);
    }
}