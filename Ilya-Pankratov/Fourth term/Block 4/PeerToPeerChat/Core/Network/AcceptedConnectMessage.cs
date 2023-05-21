using Core.Data;

namespace Core.Network;

public class AcceptedConnectMessage : NodeMessage
{
    public List<Peer> Peers { get; set; }
}