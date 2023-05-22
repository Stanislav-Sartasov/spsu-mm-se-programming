using System.Net;

namespace Core.Data;

public class Peer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IpAddress { get; set; }
    public int Port { get; set; }
}