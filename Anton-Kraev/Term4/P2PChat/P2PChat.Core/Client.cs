using System.Net;
using System.Net.Sockets;

namespace P2PChat.Core;

public class Client
{
    private static readonly IPAddress LocalHost = IPAddress.Parse("127.0.0.1");
    public IPEndPoint EndPoint { get; }
    private readonly Action<Message> _onNewMessage;

    private readonly List<IPEndPoint> _connectedPeers = new();
    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private readonly Thread _thread;

    private volatile bool _disposed;

    public Client(int port, Action<Message> onNewMessageCallback)
    {
        EndPoint = new IPEndPoint(LocalHost, port);
        _onNewMessage = onNewMessageCallback;

        _socket.Bind(EndPoint);
        _thread = new Thread(Run);
        _thread.Start();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        _connectedPeers.ForEach(peer => _socket.SendTo(
            new Message(MessageType.RemovePeer, EndPoint.ToString()).Serialize(),
            peer
        ));
        _socket.Shutdown(SocketShutdown.Both);
        _socket.Close();
        _thread.Join();
    }

    public void Connect(int port)
    {
        var endPoint = new IPEndPoint(LocalHost, port);
        var addMsg = new Message(MessageType.AddPeer, endPoint.ToString());
        var connectMsg = new Message(MessageType.Connect, EndPoint.ToString());

        _connectedPeers.Add(endPoint);
        _onNewMessage(addMsg);
        _socket.SendTo(connectMsg.Serialize(), endPoint);
    }

    public void Disconnect()
    {
        var msg = new Message(MessageType.RemovePeer, EndPoint.ToString());
        var serialized = msg.Serialize();

        foreach (var peer in _connectedPeers.ToList())
        {
            _socket.SendTo(serialized, peer);
            _onNewMessage(msg with { Sender = peer.ToString() });
        }
        _connectedPeers.Clear();
    }

    public void SendMessage(string message)
    {
        var msg = new Message(MessageType.Text, EndPoint.ToString(), message);
        var serialized = msg.Serialize();
        
        _onNewMessage(msg);
        _connectedPeers.ForEach(peer => _socket.SendTo(serialized, peer));
    }

    private void Run()
    {
        while (true)
        {
            if (_disposed) return;

            var buffer = new byte[1024];
            int size;

            try
            {
                size = _socket.Receive(buffer);
            }
            catch (SocketException)
            {
                continue;
            }

            var msg = Message.Deserialize(buffer, size);
            if (msg == null) continue;

            switch (msg.Type)
            {
                case MessageType.Connect:
                    var addMsg = new Message(MessageType.AddPeer, msg.Sender);
                    foreach (var peer in _connectedPeers)
                    {
                        _socket.SendTo(addMsg.Serialize(), peer);
                        _socket.SendTo(
                            (addMsg with {Sender = peer.ToString()}).Serialize(),
                            ParseEndPoint(addMsg.Sender)
                            );
                    }
                    _onNewMessage(addMsg);
                    _connectedPeers.Add(ParseEndPoint(addMsg.Sender));
                    break;
                case MessageType.AddPeer:
                    _onNewMessage(msg);
                    _connectedPeers.Add(ParseEndPoint(msg.Sender));
                    break;
                case MessageType.RemovePeer:
                    _onNewMessage(msg);
                    _connectedPeers.Remove(ParseEndPoint(msg.Sender));
                    break;
                case MessageType.Text:
                    _onNewMessage(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private IPEndPoint ParseEndPoint(string str)
    {
        var endPoint = str.Split(":");
        return new IPEndPoint(IPAddress.Parse(endPoint[0]), int.Parse(endPoint[1]));
    }
}