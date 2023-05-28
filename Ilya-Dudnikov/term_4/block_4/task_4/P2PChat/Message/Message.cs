using System.Diagnostics;
using System.Net;
using System.Xml.Serialization;

namespace P2PChat.Message;

public record Message(string Sender, MessageType Type, string Data)
{
    public string Sender { get; init; } = Sender;
    public MessageType Type { get; init; } = Type;
    public string Data { get; init; } = Data;

    private Message() : this("", MessageType.Message, ""){}
}