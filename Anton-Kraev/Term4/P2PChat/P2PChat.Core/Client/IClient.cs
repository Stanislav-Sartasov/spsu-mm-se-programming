using System.Net;

namespace P2PChat.Core.Client;

public interface IClient : IDisposable
{
    public void Connect(IPEndPoint endPoint);

    public void SendMessage(string message);

    public void Disconnect();
}