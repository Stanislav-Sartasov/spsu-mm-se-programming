using Core.Network;

namespace Core.Data;

public class DisconnectMessage : NodeMessage
{
    public int Port { get; set; }
}