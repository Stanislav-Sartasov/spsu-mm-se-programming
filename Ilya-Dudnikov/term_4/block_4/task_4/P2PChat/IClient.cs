using System.Net;

namespace P2PChat;

public interface IClient : IDisposable
{
    public void Join(IPEndPoint endPoint);

    public void SendMessage(string msg);

    public void Leave();
}