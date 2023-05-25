using System.Net;
using System.Xml;
using P2PChat;
using P2PChat.Message;

namespace ClientTests;

public class Tests
{
    private Client client1;
    private Client client2;
    private Client client3;
    private volatile List<String> receivedMessages;

    [SetUp]
    public void Setup()
    {
        receivedMessages = new List<string>();
        var random = new Random();
        client1 = new Client("127.0.0.1", random.Next(1, 1024), (_, s) => receivedMessages.Add(s));
        client2 = new Client("127.0.0.2", random.Next(1, 1024), (_, s) => receivedMessages.Add(s));
        client3 = new Client("127.0.0.3", random.Next(1, 1024), (_, s) => receivedMessages.Add(s));
    }

    [TearDown]
    public void Teardown()
    {
        client1.Dispose();
        client2.Dispose();
        client3.Dispose();
    }
    
    [Test]
    public void Join()
    {
        client1.Join((IPEndPoint)client2.clientEndpoint);

        Thread.Sleep(10);
        // client1.Dispose();
    }

    [Test]
    public void ThreeWayJoin()
    {
        client1.Join((IPEndPoint)client2.clientEndpoint);
        client1.Join((IPEndPoint)client3.clientEndpoint);

        Thread.Sleep(10);
        // client1.Dispose();
        // client2.Dispose();
        // client3.Dispose();
    }

    [Test]
    public void SendMessage()
    {
        client1.Join((IPEndPoint)client2.clientEndpoint);
        Thread.Sleep(100);
        client1.SendMessage("asdf");
        Thread.Sleep(100);
        Assert.That(receivedMessages[0], Is.EqualTo("asdf"));
    }
    
    [Test]
    public void ThreeWaySendMessage()
    {
        client1.Join((IPEndPoint)client2.clientEndpoint);
        client3.Join((IPEndPoint)client2.clientEndpoint);
        Thread.Sleep(100);
        client1.SendMessage("asdf");
        Thread.Sleep(100);
        Assert.That(receivedMessages[0], Is.EqualTo("asdf"));
        Assert.That(receivedMessages[1], Is.EqualTo("asdf"));
    }
}