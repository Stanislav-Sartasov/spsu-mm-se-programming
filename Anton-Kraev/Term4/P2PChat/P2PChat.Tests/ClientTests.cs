using P2PChat.Core;
using System.Net;

namespace P2PChat.Tests;

public class ClientTests
{
    private Client _client1;
    private Client _client2;
    private Client _client3;
    private List<IPEndPoint> _connectedPeers1;
    private List<Message> _messages1;
    private List<IPEndPoint> _connectedPeers2;
    private List<Message> _messages2;
    private List<IPEndPoint> _connectedPeers3;
    private List<Message> _messages3;

    private void OnNewMessage(Message msg, List<IPEndPoint> connectedPeers, List<Message> messages)
    {
        switch (msg.Type)
        {
            case MessageType.Text:
                messages.Add(msg);
                break;
            case MessageType.AddPeer:
                connectedPeers.Add(IPEndPoint.Parse(msg.Sender));
                break;
            case MessageType.RemovePeer:
                connectedPeers.Remove(IPEndPoint.Parse(msg.Sender));
                break;
        }
    }

    private void OnNewMessage1(Message msg)
    {
        OnNewMessage(msg, _connectedPeers1, _messages1);
    }

    private void OnNewMessage2(Message msg)
    {
        OnNewMessage(msg, _connectedPeers2, _messages2);
    }

    private void OnNewMessage3(Message msg)
    {
        OnNewMessage(msg, _connectedPeers3, _messages3);
    }

    [SetUp]
    public void Setup()
    {
        _connectedPeers1 = new();
        _messages1 = new();
        _connectedPeers2 = new();
        _messages2 = new();
        _connectedPeers3 = new();
        _messages3 = new();
        _client1 = new Client(1111, OnNewMessage1);
        _client2 = new Client(2222, OnNewMessage2);
        _client3 = new Client(3333, OnNewMessage3);
    }

    [TearDown]
    public void Teardown()
    {
        _client1.Dispose();
        _client2.Dispose();
        _client3.Dispose();
    }

    [Test]
    public void TwoClientsJoinTest()
    {
        _client1.Connect(2222);
        Thread.Sleep(100);

        Assert.AreEqual(1, _connectedPeers1.Count);
        Assert.AreEqual(1, _connectedPeers2.Count);
        Assert.AreEqual(_client1.EndPoint, _connectedPeers2[0]);
        Assert.AreEqual(_client2.EndPoint, _connectedPeers1[0]);
    }

    [Test]
    public void ThreeClientsJoinTest()
    {
        _client1.Connect(2222);
        _client3.Connect(1111);
        Thread.Sleep(100);

        Assert.AreEqual(2, _connectedPeers1.Count);
        Assert.AreEqual(2, _connectedPeers2.Count);
        Assert.AreEqual(2, _connectedPeers2.Count);
    }

    [Test]
    public void SendMessageTest()
    {
        _client1.Connect(2222);
        _client3.Connect(1111);
        _client2.SendMessage("Hello");
        Thread.Sleep(100);

        Assert.AreEqual(1, _messages1.Count);
        Assert.AreEqual(1, _messages2.Count);
        Assert.AreEqual(1, _messages3.Count);
        var msg = new Message(MessageType.Text, _client2.EndPoint.ToString(), "Hello");
        Assert.AreEqual(msg.Type, _messages1[0].Type);
        Assert.AreEqual(msg.Sender, _messages1[0].Sender);
        Assert.AreEqual(msg.Text, _messages1[0].Text);
    }

    [Test]
    public void DisconnectTest()
    {
        _client1.Connect(2222);
        _client3.Connect(1111);
        Thread.Sleep(100);
        _client2.Disconnect();
        Thread.Sleep(100);

        Assert.AreEqual(0, _connectedPeers2.Count);
        Assert.AreEqual(1, _connectedPeers1.Count);
        Assert.AreEqual(1, _connectedPeers3.Count);
        Assert.AreEqual(_client1.EndPoint, _connectedPeers3[0]);
        Assert.AreEqual(_client3.EndPoint, _connectedPeers1[0]);
    }
}