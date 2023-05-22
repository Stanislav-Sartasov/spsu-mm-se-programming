using System.Collections.Concurrent;
using System.Net;
using Core.Chat;
using Core.Data;

namespace Core;

public interface IClient<T>
{
    public Guid Id { get; }
    public string Name { get; }
    public ConcurrentDictionary<Guid, Peer> Peers { get; }
    public IPAddress IpAddress { get; }
    public int Port { get; }
    public void Start();
    public Task ConnectToClient(int port, IPAddress? ipAddress);
}