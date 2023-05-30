using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;

namespace P2PChat.Core;

public class Client
{
    private static readonly IPAddress LocalHost = IPAddress.Parse("127.0.0.1");
    
    public IPEndPoint EndPoint { get; }

    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    public ObservableCollection<IPEndPoint> ConnectedPeers { get; } = new();
    public ObservableCollection<Message> Messages { get; } = new();
    private readonly Thread _thread;

    private volatile bool _disposed;

    public Client(int port)
    {
        EndPoint = new IPEndPoint(LocalHost, port);
        
        _socket.Bind(EndPoint);
        _thread = new Thread(Run);
        _thread.Start();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        _socket.Shutdown(SocketShutdown.Both);
        _socket.Close();
        _thread.Join();
        ConnectedPeers.Clear();
    }

    public void Connect(int port)
    {
        var endPoint = new IPEndPoint(LocalHost, 10);
        var msg = new Message(MessageType.Connect, EndPoint);
        ConnectedPeers.Add(endPoint);
        _socket.SendTo(msg.Serialize(), endPoint);
    }

    public void Disconnect()
    {
        var msg = new Message(MessageType.RemovePeer, EndPoint).Serialize();
        foreach (var peer in ConnectedPeers)
        { 
            _socket.SendTo(msg, peer);
        }
        ConnectedPeers.Clear();
    }

    public void SendMessage(string message)
    {
        var msg = new Message(MessageType.Text, EndPoint, message);
        Messages.Add(msg);
        var serialized = msg.Serialize();
        foreach (var peer in ConnectedPeers)
        {
            _socket.SendTo(serialized, peer);
        }
    }

    private void Run()
    {
        while (true)
        {
            var buffer = new byte[1024];

            var size = _socket.Receive(buffer);
            if (size == 0) continue;

            var msg = Message.Deserialize(buffer);
            if (msg == null) continue;

            switch (msg.Type)
            {
                case MessageType.Connect:
                    var connected = msg.Sender;
                    var addMsg = new Message(MessageType.AddPeer, connected);
                    foreach (var peer in ConnectedPeers)
                    {
                        _socket.SendTo(addMsg.Serialize(), peer);
                        _socket.SendTo((addMsg with {Sender = peer}).Serialize(), connected);
                    }
                    ConnectedPeers.Add(connected);
                    break;
                case MessageType.AddPeer:
                    ConnectedPeers.Add(msg.Sender);
                    break;
                case MessageType.RemovePeer:
                    ConnectedPeers.Remove(msg.Sender);
                    break;
                case MessageType.Text:
                    Messages.Add(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}