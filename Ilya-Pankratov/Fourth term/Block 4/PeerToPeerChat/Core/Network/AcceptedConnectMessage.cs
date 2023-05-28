using Core.Chat;
using Core.Data;

namespace Core.Network;

public class AcceptedConnectMessage : NodeMessage
{
    public IEnumerable<Peer> Peers { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}