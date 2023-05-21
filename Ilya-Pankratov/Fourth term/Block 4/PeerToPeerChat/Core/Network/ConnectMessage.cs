using Core.Data;

namespace Core.Network;

public class ConnectMessage : NodeMessage
{
    public int ListenPort { get; set; }
}