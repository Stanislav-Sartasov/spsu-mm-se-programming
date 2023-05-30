using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Interop;

namespace P2PChat.Core;

public class Client
{
    private static readonly IPAddress LocalHost = IPAddress.Parse("127.0.0.1");
    public IPEndPoint EndPoint { get; }

    public ObservableCollection<IPEndPoint> ConnectedPeers { get; } = new();
    public ObservableCollection<Message> Messages { get; } = new();

    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
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

        foreach (var peer in ConnectedPeers)
        {
            _socket.SendTo(
                new Message(MessageType.RemovePeer, EndPoint.ToString()).Serialize(),
                peer
                );
        }
        _socket.Shutdown(SocketShutdown.Both);
        _socket.Close();
        _thread.Join();
    }

    public void Connect(int port)
    {
        var endPoint = new IPEndPoint(LocalHost, port);
        var msg = new Message(MessageType.Connect, EndPoint.ToString());
        App.Current.Dispatcher.Invoke(delegate
        {
            ConnectedPeers.Add(endPoint);
        });
        _socket.SendTo(msg.Serialize(), endPoint);
    }

    public void Disconnect()
    {
        var msg = new Message(MessageType.RemovePeer, EndPoint.ToString()).Serialize();
        foreach (var peer in ConnectedPeers)
        { 
            _socket.SendTo(msg, peer);
        }
        App.Current.Dispatcher.Invoke(delegate
        {
            ConnectedPeers.Clear();
            Messages.Clear();
        });
    }

    public void SendMessage(string message)
    {
        var msg = new Message(MessageType.Text, EndPoint.ToString(), message);
        App.Current.Dispatcher.Invoke(delegate
        {
            Messages.Add(msg);
        });
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
                    var connected = msg.Sender;
                    var addMsg = new Message(MessageType.AddPeer, connected);
                    foreach (var peer in ConnectedPeers)
                    {
                        _socket.SendTo(addMsg.Serialize(), peer);
                        _socket.SendTo(
                            (addMsg with {Sender = peer.ToString()}).Serialize(),
                            ParseEndPoint(connected)
                            );
                    }
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ConnectedPeers.Add(ParseEndPoint(connected));
                    });
                    break;
                case MessageType.AddPeer:
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ConnectedPeers.Add(ParseEndPoint(msg.Sender));
                    });
                    break;
                case MessageType.RemovePeer:
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        ConnectedPeers.Remove(ParseEndPoint(msg.Sender));
                    });
                    break;
                case MessageType.Text:
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        Messages.Add(msg);
                    });
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