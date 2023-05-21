using System.Net;
using System.Net.Sockets;
using Core.Chat;
using Core.Data;
using Core.Network;

namespace Core;

public class ClientNode : IClient<Message>, IDisposable
{
    public Guid Id { get; init; }
    public string Name { get; }
    public List<Peer> Peers { get; }
    public IChat<Message> Chat { get; }
    public IPAddress IpAddress { get; }
    public int Port { get; }
    private readonly Socket listenSocket;
    private const int backLog = 50;
    private volatile bool isRunning;

    public ClientNode(string name, int port)
    {
        Id = Guid.NewGuid();
        Name = name;
        Port = port;
        IpAddress = NetworkManager.GetLocalIp();
        Peers = new List<Peer>();
        Chat = new Chat<Message>();
        isRunning = true;
        listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Start()
    {
        try
        {
            var ep = new IPEndPoint(IpAddress, Port);
            listenSocket.Bind(ep);
            listenSocket.Listen(backLog);
            BeginAcceptSocket();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task ConnectToClient(int port, IPAddress? ipAddress = null)
    {
        ipAddress ??= NetworkManager.GetLocalHostIp();
        var connectEp = new IPEndPoint(ipAddress, port);
        var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await sender.ConnectAsync(connectEp);

            var connectData = new ConnectMessage()
            {
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                ListenPort = Port,
                Type = typeof(ConnectMessage)
            };

            var sendData = MessageConverter.Serialize(connectData);
            var sentBytes = await sender.SendAsync(sendData.Data);
            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SendMessage(string message)
    {
        var nodeMessage = new PeerMessage()
        {
            Name = Name,
            SentTime = DateTime.Now,
            IpAddress = IpAddress.ToString(),
            Message = message,
            Type = typeof(PeerMessage)
        };

        var sentData = MessageConverter.Serialize(nodeMessage);

        foreach (var peer in Peers)
        {
            await SendToPeer(peer, sentData.Data);
        }
    }

    public async Task Disconnect()
    {
        isRunning = false; // Called Close method at Dispose

        var nodeMessage = new DisconnectMessage
        {
            Name = Name,
            SentTime = DateTime.Now,
            IpAddress = IpAddress.ToString(),
            Port = Port,
            Type = typeof(DisconnectMessage)
        };

        var sentData = MessageConverter.Serialize(nodeMessage);

        foreach (var peer in Peers)
        {
            await SendToPeer(peer, sentData.Data);
        }
    }

    private async Task SendToPeer(Peer peer, byte[] bytes)
    {
        var ipAddress = IPAddress.Parse(peer.IpAddress);
        var connectEp = new IPEndPoint(ipAddress, peer.Port);
        var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await sender.ConnectAsync(connectEp);
            var sentBytes = await sender.SendAsync(bytes);
            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void BeginAcceptSocket()
    {
        if (isRunning)
        {
            var nodeData = new ReceivedData();
            listenSocket.BeginAccept(AcceptCallBack, nodeData);
        }
    }

    private void AcceptCallBack(IAsyncResult result)
    {
        try
        {
            if (!isRunning) // Peer is disconnected. 
            {
                return;
            }

            var handler = listenSocket.EndAccept(result);
            Console.WriteLine($"{Name}: Accept connection from {handler.RemoteEndPoint}");
            var nodeData = result.AsyncState as ReceivedData;
            nodeData.Handler = handler;
            handler.BeginReceive(nodeData.Buffer, 0, nodeData.BufferSize, 0, ReceiveCallback, nodeData);
            BeginAcceptSocket();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        var nodeData = result.AsyncState as ReceivedData;
        var handler = nodeData.Handler;
        var numberBytes = handler.EndReceive(result);
        nodeData.SaveReceivedData();

        if (numberBytes < nodeData.BufferSize) // End Receive
        {
            handler.Close();
            ProcessData(nodeData); // TODO: think
        }
        else // Continue Receive
        {
            handler.BeginReceive(nodeData.Buffer, 0, nodeData.BufferSize, 0, ReceiveCallback, nodeData);
        }
    }

    private async void ProcessData(ReceivedData data)
    {
        var receiptTime = DateTime.Now;
        var nodeInformation = MessageConverter.Deserialize<NodeMessage>(data.GetBytes);

        if (nodeInformation.Type == typeof(PeerMessage))
        {
            var message = MessageConverter.Deserialize<PeerMessage>(data.GetBytes);
            message.ReceiptTime = receiptTime;
            ProcessMessage(message);
        }
        else if (nodeInformation.Type == typeof(ConnectMessage))
        {
            var connectInfo = MessageConverter.Deserialize<ConnectMessage>(data.GetBytes);
            connectInfo.ReceiptTime = receiptTime;
            await AcceptConnection(connectInfo);
        }
        else if (nodeInformation.Type == typeof(AcceptedConnectMessage))
        {
            var acceptedConnectionInfo = MessageConverter.Deserialize<AcceptedConnectMessage>(data.GetBytes);
            acceptedConnectionInfo.ReceiptTime = receiptTime;
            ConfirmAcceptedConnection(acceptedConnectionInfo);
        }
        else if (nodeInformation.Type == typeof(DisconnectMessage))
        {
            var disconnect = MessageConverter.Deserialize<DisconnectMessage>(data.GetBytes);
            disconnect.ReceiptTime = receiptTime;
            DisconnectPeer(disconnect);
        }
        else
        {
            throw new InvalidDataException();
        }
    }

    private void DisconnectPeer(DisconnectMessage disconnect)
    {
        Peers.RemoveAll(x =>
            x.IpAddress == disconnect.IpAddress && x.Name == disconnect.Name && x.Port == disconnect.Port);
    }

    private void ProcessMessage(PeerMessage message)
    {
        Console.WriteLine($"{Name}: Get message from {message.Name}:\n{message.Message}");
        
        var chatMessage = new Message
        {
            PeerName = message.Name,
            SentTime = message.SentTime,
            Content = message.Message
        };

        Chat.SaveMessage(chatMessage);
    }

    private async Task AcceptConnection(ConnectMessage connect)
    {
        var ipAddress = IPAddress.Parse(connect.IpAddress);
        var connectEp = new IPEndPoint(ipAddress, connect.ListenPort);
        var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        Console.WriteLine($"{Name}: Approved connection from {connect.Name}");
        
        try
        {
            await sender.ConnectAsync(connectEp);
            var connectData = new AcceptedConnectMessage()
            {
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                Peers = new List<Peer>(Peers),
                Type = typeof(AcceptedConnectMessage)
            };

            connectData.Peers.Add(new Peer
            {
                Name = Name,
                IpAddress = connect.IpAddress,
                Port = Port
            });

            var sendData = MessageConverter.Serialize(connectData);
            var sentBytes = await sender.SendAsync(sendData.Data);

            var connectedPeer = new Peer
            {
                Name = connect.Name,
                IpAddress = connect.IpAddress,
                Port = connect.ListenPort
            };
            Peers.Add(connectedPeer);

            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void ConfirmAcceptedConnection(AcceptedConnectMessage acceptedConnect)
    {
        Console.WriteLine($"{Name}: Get confirmation for connection from {acceptedConnect.Name}");
        Peers.AddRange(acceptedConnect.Peers);
    }

    public void Dispose()
    {
        // Dispose is same as the Close overload without the timeout argument.

        /*
         * When the Close method is called while an asynchronous operation is in progress,
         * the callback provided to the BeginAccept method is called. A subsequent call to
         * the EndAccept method will throw an ObjectDisposedException (before .NET 7) or a
         * SocketException (on .NET 7+) to indicate that the operation has been cancelled.
        */

        listenSocket.Close();
    }
}