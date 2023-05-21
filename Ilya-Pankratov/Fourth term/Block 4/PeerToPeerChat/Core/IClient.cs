using System.Net;
using Core.Chat;
using Core.Data;

namespace Core;

public interface IClient<T>
{
    public Guid Id { get; }
    public string Name { get; }
    public List<Peer> Peers { get; }
    public IChat<T> Chat { get; }
    public IPAddress IpAddress { get; }
    public int Port { get; }
    public void Start();
    public Task ConnectToClient(int port, IPAddress? ipAddress);
}