using System.Net;

namespace P2PChat.Core.Client;

public class TcpClient : IClient
{
    public void Connect(IPEndPoint endPoint)
    {
        throw new NotImplementedException();
    }

    public void Disconnect()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public void SendMessage(string message)
    {
        throw new NotImplementedException();
    }
}