using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Core.Chat;
using Core.Data;
using Core.Network;

namespace Core;

public class ClientNode : IClient<Message>, IDisposable
{
    public Guid Id { get; }
    public string Name { get; }
    public IPAddress IpAddress { get; }
    public int Port { get; private set; }
    public ConcurrentDictionary<Guid, Peer> Peers { get; }


    // Client state
    private volatile bool isRunning;

    // Listen socket
    private readonly Socket listenSocket;
    private const int backLog = 50;

    // Message dealing
    private Action<Message> onMessageReceived;

    // Connections
    private ConcurrentDictionary<IPEndPoint, DateTime> startedConnections;
    private volatile bool isConnecting;
    private const int delayTimer = 25;
    private const int maxDelay = 100;
    private const int maxConnectionTime = 5;
    private object connectionState = new();

    public ClientNode(string name, int port, Action<Message> onMessageReceived, IPAddress? ipAddress = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Port = port;
        IpAddress = ipAddress ?? NetworkManager.GetLocalHostIp();
        Peers = new ConcurrentDictionary<Guid, Peer>();
        isRunning = true;
        listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.onMessageReceived = onMessageReceived;
        startedConnections = new ConcurrentDictionary<IPEndPoint, DateTime>();
        isConnecting = false;
    }

    private void CheckStartedConnections()
    {
        /*
         * Remove clients from connect list if their answer time is more than 10 seconds
         */

        if (isConnecting)
        {
            foreach (var data in startedConnections)
            {
                if ((DateTime.Now - data.Value).Seconds > maxConnectionTime)
                {
                    startedConnections.TryRemove(data);
                }
            }

            if (startedConnections.IsEmpty)
            {
                lock (connectionState)
                {
                    isConnecting = false;
                }
            }
        }
    }

    public void Start() // Launch listening activity on IpAddress:Port
    {
        try
        {
            var ep = new IPEndPoint(IpAddress, Port);
            listenSocket.Bind(ep);

            if (Port < 1)
            {
                if (listenSocket.LocalEndPoint is IPEndPoint localIpEndPoint)
                {
                    Port = localIpEndPoint.Port;
                }
            }

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
        var time = DateTime.Now;

        if (startedConnections.GetOrAdd(connectEp, time) != time)
        {
            return; // Connection request was already sent
        }

        lock (connectionState)
        {
            isConnecting = true;
        }

        var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await sender.ConnectAsync(connectEp);

            var connectData = new ConnectMessage()
            {
                Id = Id,
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                ListenPort = Port,
                Type = typeof(ConnectMessage)
            };

            var sendData = MessageConverter.Serialize(connectData);
            await sender.SendAsync(sendData.Data);
            sender.Shutdown(SocketShutdown.Both);
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
        var chatMessage = new Message
        {
            PeerName = Name,
            SentTime = DateTime.Now,
            Content = message
        };

        var delayCounter = 0;
        while (isConnecting || delayCounter > maxDelay) // Waits for the connection to end
        {
            await Task.Delay(delayTimer);
            delayCounter++;
        }

        if (isConnecting)
        {
            CheckStartedConnections();
        }

        await SendMessageToPeers(chatMessage);
    }

    private async Task SendMessageToPeers(Message message)
    {
        var nodeMessage = new PeerMessage()
        {
            Name = Name,
            SentTime = message.SentTime,
            IpAddress = IpAddress.ToString(),
            Message = message.Content,
            Type = typeof(PeerMessage)
        };

        var sentData = MessageConverter.Serialize(nodeMessage);

        foreach (var peer in Peers.Values)
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

        foreach (var peer in Peers.Values)
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
            await sender.SendAsync(bytes);
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
        Peers.TryRemove(disconnect.Id, out _);
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

        onMessageReceived(chatMessage);
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
                Id = Id,
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                Peers = new List<Peer>(Peers.Values),
                Type = typeof(AcceptedConnectMessage)
            };

            var sendData = MessageConverter.Serialize(connectData);
            var sentBytes = await sender.SendAsync(sendData.Data);

            var connectedPeer = new Peer
            {
                Id = connect.Id,
                Name = connect.Name,
                IpAddress = connect.IpAddress,
                Port = connect.ListenPort
            };

            if (Peers.TryAdd(connectedPeer.Id, connectedPeer))
            {
            }

            sender.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task ConfirmAcceptedConnection(AcceptedConnectMessage acceptedConnect)
    {
        Console.WriteLine($"{Name}: Get confirmation for connection from {acceptedConnect.Name}");
        foreach (var nextPeer in acceptedConnect.Peers)
        {
            if (Peers.ContainsKey(nextPeer.Id)) continue;
            await ConnectToClient(nextPeer.Port, IPAddress.Parse(nextPeer.IpAddress));
        }

        var peer = new Peer
        {
            Id = acceptedConnect.Id,
            IpAddress = acceptedConnect.IpAddress,
            Name = acceptedConnect.Name,
            Port = acceptedConnect.Port
        };

        if (Peers.TryAdd(acceptedConnect.Id, peer))
        {
            var ip = IPAddress.Parse(acceptedConnect.IpAddress);
            var ep = new IPEndPoint(ip, acceptedConnect.Port);
            startedConnections.TryRemove(ep, out _);
        }
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