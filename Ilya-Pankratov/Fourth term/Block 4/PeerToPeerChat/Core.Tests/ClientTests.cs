using System.Net;
using Core.Chat;
using Core.Network;

namespace Core.Tests;

public class ClientTests
{
    private object locker = new object();

    private void OnMessageReceived(Message message)
    {
    }

    private IPEndPoint GetFreeLocalEndPoint()
    {
        var ip = NetworkManager.GetLocalHostIp();
        return new IPEndPoint(ip, 0);
    }

    private ClientNode GetNewClient(Action<Message> onMessageReceived)
    {
        var endPoint = GetFreeLocalEndPoint();
        return new ClientNode(Guid.NewGuid().ToString(), endPoint.Port, onMessageReceived, endPoint.Address);
    }

    private string[] Messages =
    {
        "Hello, Dear!", "Good Morning!", "How are you?", "What's up?", "How do you do?", "Nice to meet you!",
        "Good to see you", "Hiya", "We Need To Go Deeper", "What, is there a pharmacy down there?", "YOU BROKE REDDIT"
    };

    [Test]
    public async Task SingleChatTest()
    {
    }
}