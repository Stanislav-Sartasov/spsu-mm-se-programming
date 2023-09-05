using Core.Chat;

namespace Core.Tests;

public class ClientTests
{
    private object locker = new object();
    private int freePort = 0;
    private int timeout = 5000;

    private void OnMessageReceived(List<Message> messages, Message message)
    {
        lock (locker)
        {

            var messageCtx = message.Content;
            messages.Add(message);
            var count = messages.Count;
            var test = 123;
        }
    }

    private ClientNode GetNewClient()
    {
        return new ClientNode(Guid.NewGuid().ToString());
    }

    private string[] Messages =
    {
        "Hello, Dear!", "Good Morning!", "How are you?", "What's up?", "How do you do?", "Nice to meet you!", "Wow",
        "Good to see you", "Hiya", "We Need To Go Deeper", "What, is there a pharmacy down there?", "YOU BROKE REDDIT"
    };

    [Test]
    public async Task SingleChatTest()
    {
        var client = GetNewClient();
        client.Start(freePort);
        await client.SendMessage(Messages[0]);
        client.Stop();
        Assert.Pass();
    }

    [Test]
    public async Task ThreePeersTest()
    {
        var client1 = GetNewClient();
        var client2 = GetNewClient();
        var client3 = GetNewClient();
        client1.Start(freePort);
        client2.Start(freePort);
        client3.Start(freePort);

        // Collection for receiving messages
        var messages1 = new List<Message>();
        var messages2 = new List<Message>();
        var messages3 = new List<Message>();

        // Set OnMessagaeReceiveMethod
        client1.OnMessageReceived = message => OnMessageReceived(messages1, message);
        client2.OnMessageReceived = message => OnMessageReceived(messages2, message);
        client3.OnMessageReceived = message => OnMessageReceived(messages3, message);

        // Connect client 1 to client 2
        var port2 = client2.Port;
        var ip2 = client2.IpAddress;
        await client1.ConnectToClient(port2, ip2, timeout);

        Thread.Sleep(1000);

        // Connect client 3 to client 1
        var port1 = client1.Port;
        var ip1 = client1.IpAddress;
        await client3.ConnectToClient(port1, ip1, timeout);

        // SendMessages
        foreach (var message in Messages)
        {
            await client1.SendMessage(message);
        }

        foreach (var message in Messages)
        {
            await client2.SendMessage(message);
        }

        foreach (var message in Messages)
        {
            await client3.SendMessage(message);
        }

        Thread.Sleep(1000);

        await client1.Disconnect();
        await client2.Disconnect();
        await client3.Disconnect();
        client1.Stop();
        client2.Stop();
        client3.Stop();

        // TODO: Пофиксить слипшиеся сообщения и этот тест

        Assert.That(messages1.Count, Is.EqualTo(Messages.Length * 2));
        Assert.That(messages2.Count, Is.EqualTo(Messages.Length * 2));
        Assert.That(messages3.Count, Is.EqualTo(Messages.Length * 2));

        foreach (var message in messages1)
        {
            Assert.That(Messages, Does.Contain(message.Content));
        }

        foreach (var message in messages2)
        {
            Assert.That(Messages, Does.Contain(message.Content));
        }
    }
}