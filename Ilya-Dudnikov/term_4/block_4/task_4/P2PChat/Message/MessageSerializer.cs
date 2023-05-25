using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace P2PChat.Message;

public class MessageSerializer
{
    public byte[] Serialize(Message message)
    {
        return Encoding.Default.GetBytes(JsonSerializer.Serialize(message));
    }
    
    public Message Deserialize(byte[] serializedBytes, int byteCount)
    {
        var json = Encoding.Default.GetString(serializedBytes, 0, byteCount);
        var message = JsonSerializer.Deserialize<Message>(json);

        ArgumentNullException.ThrowIfNull(message);
        return message;
    }
}