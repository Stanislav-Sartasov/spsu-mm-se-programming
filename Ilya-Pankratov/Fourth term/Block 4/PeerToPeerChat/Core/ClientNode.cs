using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using Core.Chat;
using Core.Data;
using Core.Network;

namespace Core;

public class ClientNode : IClient<Message, Peer>
{
    public Guid Id { get; }
    public string Name { get; }
    public IPAddress IpAddress { get; }
    public int Port { get { return port; } private set { port = value; } }
    private volatile int port;
    public ConcurrentDictionary<Guid, Peer> Peers { get; } // Connected Peers

    // Event actions
    public Action? OnConnectionSuccessed { get; set; }
    public Action? OnConnectionFailed { get; set; }
    public Action<Message>? OnMessageReceived { get; set; }
    public Action<Peer>? OnNewConnection { get; set; }
    public Action<Peer>? OnDisconnection { get; set; }

    // ClientNode state
    private volatile bool isRunning;
    private object runningState = new();

    // Listen socket
    private Socket listenSocket;
    private const int backLog = 50;

    // Connections
    private ConcurrentDictionary<IPEndPoint, DateTime> startedConnections;
    private volatile bool isConnecting;
    private volatile bool isConnected;
    private volatile bool isAborted;
    private volatile int millisecondsConnectionTimeout;
    private int maxMessageDelay = 5000;
    private int delayInterval = 100;
    private object connectionState = new();
    private int expectedPeers;

    public ClientNode(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Port = 0;
        IpAddress = NetworkManager.GetLocalIp();
        Peers = new ConcurrentDictionary<Guid, Peer>();
        isRunning = false;
        listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        startedConnections = new ConcurrentDictionary<IPEndPoint, DateTime>();
        isConnecting = false;
        isConnected = false;
        expectedPeers = 0;
        isAborted = false;
    }

    private void StartTimeout(int millisecondsTimeout)
    {
        var time = DateTime.Now;
        Thread.Sleep(millisecondsTimeout);

        while (!isConnected)
        {
            if ((DateTime.Now - time).TotalMilliseconds > millisecondsTimeout && !isConnected)
            {
                AbortConnection();
                break;
            }
        }
    }

    private async void AbortConnection()
    {
        lock (connectionState)
        {
            isConnected = false;
            isConnecting = false;
            isAborted = true;
        }

        await Disconnect();
        OnConnectionFailed?.Invoke();
    }

    public void Start(int port) // Launch listening activity on IpAddress:Port
    {
        lock (runningState)
        {
            if (isRunning)
            {
                return;
            }
            else
            {
                Port = port;
                isRunning = true;
            }
        }

        try
        {
            Port = port;
            var ep = new IPEndPoint(IpAddress, Port);
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
        catch (Exception ex)
        {
            if (!isAborted)
            {
                AbortConnection();
            }
        }
    }

    public async Task ConnectToClient(int port, IPAddress? ipAddress, int millisecondsTimeout)
    {
        if (isConnected)
        {
            return;
        }

        if (!isConnecting)
        {
            lock (connectionState)
            {
                if (!isConnecting)
                {
                    isConnecting = true;
                    isConnected = false;

                    // Launch timeout thread
                    millisecondsConnectionTimeout = millisecondsTimeout;
                    var thread = new Thread(() => StartTimeout(millisecondsTimeout));
                    thread.Start();
                }
            }
        }

        ipAddress ??= NetworkManager.GetLocalHostIp();
        var connectEp = new IPEndPoint(ipAddress, port);
        var time = DateTime.Now;

        if (startedConnections.GetOrAdd(connectEp, time) != time)
        {
            return; // Connection request was already sent
        }

        try
        {
            var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
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
        catch (Exception ex)
        {
            if (!isAborted)
            {
                AbortConnection();
            }
        }
    }

    public async Task SendMessage(string message)
    {
        var enteredTime = DateTime.Now;
        while (!isConnected)
        {
            Thread.Sleep(delayInterval);

            if ((DateTime.Now - enteredTime).TotalMilliseconds > maxMessageDelay)
            {
                return;
            }
        }

        var chatMessage = new Message
        {
            PeerName = Name,
            SentTime = DateTime.Now,
            Content = message
        };

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
        var nodeMessage = new DisconnectMessage
        {
            Id = Id,
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

        lock (connectionState)
        {
            isConnected = false;
            isConnecting = false;
        }

        Peers.Clear();
        startedConnections.Clear();
        expectedPeers = 0;
    }

    public void Stop()
    {
        lock (runningState)
        {
            isRunning = false;
            listenSocket.Close();
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
        catch (Exception ex)
        {
            if (!isAborted)
            {
                AbortConnection();
            }
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
            if (!isRunning) // ClientNode is disconnected. 
            {
                return;
            }

            var handler = listenSocket.EndAccept(result);
            var nodeData = result.AsyncState as ReceivedData;
            nodeData.Handler = handler;
            handler.BeginReceive(nodeData.Buffer, 0, nodeData.BufferSize, 0, ReceiveCallback, nodeData);
            BeginAcceptSocket();
        }
        catch (Exception e)
        {
            AbortConnection();
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
            ProcessData(nodeData);
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
            await ConfirmAcceptedConnection(acceptedConnectionInfo);
        }
        else if (nodeInformation.Type == typeof(DisconnectMessage))
        {
            var disconnect = MessageConverter.Deserialize<DisconnectMessage>(data.GetBytes);
            disconnect.ReceiptTime = receiptTime;
            DisconnectPeer(disconnect);
        }
        else
        {
            AbortConnection();
        }
    }

    private void DisconnectPeer(DisconnectMessage disconnect)
    {
        Peers.TryRemove(disconnect.Id, out _);

        var peer = new Peer()
        {
            Id = disconnect.Id,
            Name = disconnect.Name,
            IpAddress = disconnect.IpAddress,
            Port = disconnect.Port
        };
        OnDisconnection?.Invoke(peer);
    }

    private void ProcessMessage(PeerMessage message)
    {
        var chatMessage = new Message
        {
            PeerName = message.Name,
            SentTime = message.SentTime,
            Content = message.Message
        };

        OnMessageReceived?.Invoke(chatMessage);
    }

    private async Task AcceptConnection(ConnectMessage connect)
    {
        var ipAddress = IPAddress.Parse(connect.IpAddress);
        var connectEp = new IPEndPoint(ipAddress, connect.ListenPort);
        var sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            await sender.ConnectAsync(connectEp);
            var connectData = new AcceptedConnectMessage()
            {
                Id = Id,
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                Port = Port,
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

            Peers.TryAdd(connectedPeer.Id, connectedPeer);
            sender.Close();
            OnNewConnection?.Invoke(connectedPeer);

            if (!isConnected)
            {
                lock (connectionState)
                {
                    if (!isConnected)
                    {
                        isConnected = true;
                        isConnecting = false;
                    }
                }
            }
        }
        catch (Exception e)
        {
            AbortConnection();
        }
    }

    private async Task ConfirmAcceptedConnection(AcceptedConnectMessage acceptedConnect)
    {
        var ip = IPAddress.Parse(acceptedConnect.IpAddress);
        var ep = new IPEndPoint(ip, acceptedConnect.Port);

        if (!startedConnections.ContainsKey(ep))
        {
            // We do not sent connection to this peer
            return;
        }

        if (!isConnecting)
        {
            // Timeout. Disconnect from connected peer.

            var nodeMessage = new DisconnectMessage
            {
                Name = Name,
                SentTime = DateTime.Now,
                IpAddress = IpAddress.ToString(),
                Port = Port,
                Type = typeof(DisconnectMessage)
            };


            var peerToDisconnectFrom = new Peer
            {
                Id = Id,
                Name = Name,
                IpAddress = IpAddress.ToString(),
                Port = Port
            };

            var sentData = MessageConverter.Serialize(nodeMessage);
            await SendToPeer(peerToDisconnectFrom, sentData.Data);
        }

        if (Peers.IsEmpty)
        {
            // Create first connection.
            expectedPeers = 1 + acceptedConnect.Peers.Count(); // Number of the participants at the connecting chat
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
            startedConnections.TryRemove(ep, out _);
            OnNewConnection?.Invoke(peer);
        }

        foreach (var nextPeer in acceptedConnect.Peers)
        {
            ip = IPAddress.Parse(nextPeer.IpAddress);
            ep = new IPEndPoint(ip, nextPeer.Port);

            if (Peers.ContainsKey(nextPeer.Id) || startedConnections.ContainsKey(ep)) continue;
            await ConnectToClient(nextPeer.Port, IPAddress.Parse(nextPeer.IpAddress), millisecondsConnectionTimeout);
        }

        if (!startedConnections.Any() && Peers.Count == expectedPeers)
        {
            lock (connectionState)
            {
                isConnected = true;
                isConnecting = false;
            }

            OnConnectionSuccessed?.Invoke();
        }
    }
}