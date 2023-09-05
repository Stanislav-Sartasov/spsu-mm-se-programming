using System.Collections.Concurrent;
using System.Net;

namespace Core;

public interface IClient<TMessage, VPeer>
{
    // Client Data
    public Guid Id { get; }
    public string Name { get; }
    public ConcurrentDictionary<Guid, VPeer> Peers { get; }
    public IPAddress IpAddress { get; }
    public int Port { get; }

    // Event handlers
    public Action? OnConnectionSuccessed { get; set; }
    public Action? OnConnectionFailed { get; set; }
    public Action<TMessage>? OnMessageReceived { get; set; }
    public Action<VPeer>? OnNewConnection { get; set; }
    public Action<VPeer>? OnDisconnection { get; set; }

    // Client' methods
    public void Start(int port);
    public void Stop();
    public Task ConnectToClient(int port, IPAddress? ipAddress, int millisecondsTimeout);
    public Task Disconnect();
    public Task SendMessage(string message);
}