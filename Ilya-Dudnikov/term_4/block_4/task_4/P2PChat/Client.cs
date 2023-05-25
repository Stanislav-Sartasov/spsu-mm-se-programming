using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using P2PChat.Message;

namespace P2PChat;

public class Client : IClient
{
    public EndPoint clientEndpoint { get; protected set; }
    private List<EndPoint> peerList;
    private Socket socket;
    private Action<string, string> messageAction;
    private volatile MessageSerializer serializer;
    private volatile bool isStopped;
    private object lockObject;
    private Thread thread;

    public Client(EndPoint clientEndpoint, Action<string, string> messageAction)
    {
        this.clientEndpoint = clientEndpoint;
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        peerList = new List<EndPoint>();
        this.messageAction = messageAction;
        serializer = new MessageSerializer();
        lockObject = new();
        socket.Bind(clientEndpoint);
        socket.ReceiveTimeout = 1000;

        thread = new Thread(Run);
        thread.Start();
    }

    public Client(string ip, int port, Action<string, string> messageAction) : this(new IPEndPoint(IPAddress.Parse(ip), port), messageAction)
    {
    }

    public void Dispose()
    {
        lock (lockObject)
        {
            isStopped = true;
        }

        thread.Join();
        
        Leave();
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
        peerList.Clear();
    }

    public void Join(IPEndPoint peerEndPoint)
    {
        var message = new Message.Message(clientEndpoint.ToString(), MessageType.Join, clientEndpoint.ToString());
        socket.SendTo(serializer.Serialize(message), peerEndPoint);
    }

    public void Join(string peerAddress, int peerPort)
    {
        var peerEndPoint = new IPEndPoint(IPAddress.Parse(peerAddress), peerPort);
        Join(peerEndPoint);
    }

    public void Join(string peerAddress)
    {
        var ipPort = peerAddress.Split(':');
        Join(ipPort[0], Int32.Parse(ipPort[1]));
    }

    public void SendMessage(string msg)
    {
        messageAction(clientEndpoint.ToString(), msg);
        var message = new Message.Message(clientEndpoint.ToString(), MessageType.Message, msg);
        var serialized = serializer.Serialize(message);

        peerList.ForEach(peer => socket.SendTo(serialized, peer));
    }

    public void Leave()
    {
        var message = new Message.Message(clientEndpoint.ToString(), MessageType.Leave, clientEndpoint.ToString());
        var serialized = serializer.Serialize(message);
        peerList.ForEach(peer => socket.SendTo(serialized, peer));
    }

    private void AddPeer(IPEndPoint peerEndPoint)
    {
        var memberList = new Message.Message(clientEndpoint.ToString(), MessageType.MembersList, String.Join(";", peerList.Append(clientEndpoint)));
        socket.SendTo(serializer.Serialize(memberList), peerEndPoint);
        
        var message = new Message.Message(clientEndpoint.ToString(), MessageType.Notify, peerEndPoint.ToString());
        var serialized = serializer.Serialize(message);
        peerList.ForEach(peer => socket.SendTo(serialized, peer));
        peerList.Add(peerEndPoint);
    }

    private void RemovePeer(IPEndPoint peerEndPoint)
    {
        peerList.Remove(peerEndPoint);
    }

    private void Notify(IPEndPoint peerEndPoint)
    {
        peerList.Add(peerEndPoint);
    }

    private void ReceiveMemberList(Message.Message message)
    {
        if (message.Type != MessageType.MembersList)
        {
            throw new InvalidOperationException("Message is not a list of p2p peers");
        }

        if (message.Data != "")
        {
            peerList = new List<EndPoint>(message.Data.Split(';').ToList().Select(endPoint =>
            {
                var address = endPoint.Split(':');
                return new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
            }).ToList());
        }
        
        Console.Error.WriteLine($"members list for {clientEndpoint}");
        peerList.ForEach(Console.Error.WriteLine);
    }
    
    private void Run()
    {
        Console.WriteLine($"Running client {clientEndpoint}");
        byte[] buffer = new byte[1024];

        while (true)
        {
            lock (lockObject)
            {
                if (isStopped)
                    return;
            }

            int byteCount;
            try
            {
                byteCount = socket.Receive(buffer);
            }
            catch (SocketException _)
            {
                continue;
            }
            var message = serializer.Deserialize(buffer, byteCount);
            // Console.WriteLine($"{clientEndpoint} received {message}");
            switch (message.Type)
            {
                case MessageType.Join:
                {
                    var endPoint = message.Data.Split(':');
                    AddPeer(new IPEndPoint(IPAddress.Parse(endPoint[0]), Int32.Parse(endPoint[1])));
                }
                    break;
                case MessageType.Message:
                    messageAction(message.Sender, message.Data);
                    break;
                case MessageType.Leave:
                {
                    var endPoint = message.Data.Split(':');
                    RemovePeer(new IPEndPoint(IPAddress.Parse(endPoint[0]), Int32.Parse(endPoint[1])));
                }
                    break;
                case MessageType.MembersList:
                    ReceiveMemberList(message);
                    break;
                case MessageType.Notify:
                {
                    var endPoint = message.Data.Split(':');
                    Notify(new IPEndPoint(IPAddress.Parse(endPoint[0]), Int32.Parse(endPoint[1])));
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}