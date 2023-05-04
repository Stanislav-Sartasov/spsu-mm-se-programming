using System.Net;
using System.Net.Sockets;
using P2PChat.Core.GroupNetworking;
using P2PChat.Core.Log;
using P2PChat.Core.Message.Fallback;
using P2PChat.Core.Message.UserMessage;
using P2PChat.Core.Networking;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.Chat;

/// <summary>
///     Class <c>Chat</c> encapsulates a message exchanging node.
/// </summary>
public class Chat : IChat
{
	private readonly List<IExchangeSocket> _children;
	private readonly IClientSocket _client;
	private readonly GroupListener _groupListener;
	private readonly GroupSender _groupSender;
	private readonly IListenerSocket _listener;
	private readonly Thread _listenerThread;
	private readonly object _lock;

	private readonly ILogger _logger;
	private readonly List<UserMessage> _messages;
	private IPEndPoint? _fallback;
	private string _name;
	private IExchangeSocket? _parentConnection;

	private IPEndPoint? _parentListener;
	/*
	 * The chat is working with sockets
	 * The inner structure of several interconnected chats looks like a tree graph.
	 * Every chat except one has a parent and some have children.
	 * Hierarchy is defined by the Join method. Chat that joined becomes a child of chat it joined
	 * When one chat fails or normally exits, fallback address is used by its children to join a new chat.
	 *
	 * Fallback is defined:
	 * - if node has parent as a parent chat listener IP
	 * - if node has no parent (is a root) as a first child
	 *   first child is given no fallback IP
	 */

	private volatile bool _running;

	public Chat(ILogger logger)
	{
		_running = false;
		_lock = new object();
		_parentConnection = null;
		_client = new ClientSocket(logger);
		_name = "User";
		Port = FreePortFinder.FindFreePort();
		_logger = logger;
		_groupListener = new GroupListener(_lock);
		_groupSender = new GroupSender(_lock);
		_children = new List<IExchangeSocket>();
		_listener = new ListenerSocket(Port, _logger);
		_listenerThread = new Thread(ListenWork);
		_groupListener.OnMessageReceive += DispatchMessage;
		_groupListener.OnSocketError += HandleSocketError;
		_messages = new List<UserMessage>();
	}

	public int Port { get; }

	/// <summary>
	///     Start chat listening thread
	/// </summary>
	public void Start()
	{
		_running = true;
		StartListening();
	}

	/// <summary>
	///     Send message to all chats in the network
	/// </summary>
	/// <param name="message">Message</param>
	/// <returns>true if operation was successful and false otherwise</returns>
	public bool Send(string message)
	{
		if (!_running)
			return false;

		if (_parentConnection is null && _children.Count == 0)
			return false;

		SendMessage(message);

		return true;
	}

	/// <summary>
	///     Join chat via IP and Port
	/// </summary>
	/// <param name="address">Chat listener endpoint</param>
	/// <returns>true if operation was successful and false otherwise</returns>
	public bool Join(IPEndPoint address)
	{
		if (Equals(address.Address, IPAddress.Parse("127.0.0.1")) && address.Port == Port)
			return false;

		if (!_running)
			return false;

		var connection = ConnectTo(address);

		SetParent(address, connection);
		SendFallbackRequest();
		UpdateChildrenFallback();
		AddSocketToNetwork(connection);

		return true;
	}

	/// <summary>
	///     Change name. Name is attached to every message sent
	/// </summary>
	/// <param name="name">New name</param>
	public void ChangeName(string name)
	{
		_name = name;
	}

	/// <summary>
	///     Stop chat listening and destroy all connections
	///     Equivalent to calling Dispose
	/// </summary>
	public void Stop()
	{
		_running = false;

		_listener.Cancel();
		if (_listenerThread.IsAlive)
			_listenerThread.Join();
		
		_groupListener.Join();
	}

	/// <summary>
	///     List of sent and received messages
	/// </summary>
	/// <returns>List of messages</returns>
	public List<UserMessage> GetHistory()
	{
		return _messages;
	}

	/// <summary>
	///     Invoked every time new message is received
	/// </summary>
	public event MessageReceive? OnMessage;

	/// <summary>
	///     Stop chat listening and destroy all connections
	///     Equivalent to calling Stop
	/// </summary>
	public void Dispose()
	{
		_running = false;
		Stop();
	}

	private void DispatchMessage(IExchangeSocket socket, string message)
	{
		var senderIp = socket.GetEndpoint()!;

		if (UserMessage.IsValid(message))
		{
			var userMessage = UserMessage.FromJson(message)!;

			_groupSender.DuplicateMessage(userMessage, senderIp);

			OnMessage?.Invoke(userMessage);

			_messages.Add(userMessage);

			_logger.Log($"Received message {userMessage.Content} from {userMessage.Name}");
		}


		else if (FallbackListenerRequest.IsValid(message))
		{
			var fallback = GetFallbackAddress(socket);

			socket.Send(fallback);

			_logger.Log($"Sending {fallback.ListenerEndpoint.Endpoint} as fallback to {socket.GetEndpoint()}");
		}


		else if (MyPortRequest.IsValid(message))
		{
			var portResponse = new MyPortResponse(Port);

			socket.Send(portResponse);

			_logger.Log($"Sending {portResponse.Port} as port to {socket.GetEndpoint()}");
		}


		else if (ListenerResponse.IsValid(message))
		{
			var response = ListenerResponse.FromJson(message)!;

			_fallback = response.HasFallback ? response.ListenerEndpoint.Endpoint : null;

			_logger.Log($"Updated {_fallback} , got it from {socket.GetEndpoint()}");
		}


		else if (SkipRequest.IsValid(message))
		{
			socket.Send(new SkipMessage());
		}
	}

	private ListenerResponse GetFallbackAddress(IExchangeSocket socket)
	{
		// If chat has parent return parent
		if (_parentListener is not null)
			return new ListenerResponse(true, _parentListener);

		var firstChildNotSender = _children.FirstOrDefault();

		// If chat has children and asking is first child return nothing
		if (firstChildNotSender is null || firstChildNotSender == socket)
			return new ListenerResponse(false, IPEndPoint.Parse("0.0.0.0:0"));

		// Else ask first child for their listener
		firstChildNotSender.Send(new SkipRequest());
		Thread.Sleep(10);
		firstChildNotSender.Wait();
		Thread.Sleep(10);
		firstChildNotSender.Send(new MyPortRequest());
		var childPortString = firstChildNotSender.Receive();
		var childPort = MyPortResponse.FromJson(childPortString)!.Port;
		var address = firstChildNotSender.GetEndpoint()!.Address;
		var endpoint = new IPEndPoint(address, childPort);
		return new ListenerResponse(true, endpoint);
	}

	private void HandleSocketError(IExchangeSocket socket, SocketException exception)
	{
		_logger.Log($"Exception occurred in {socket.GetEndpoint()}");

		if (socket == _parentConnection && _fallback is not null && _fallback.Port != 0)
			Join(_fallback);

		if (socket == _parentConnection)
			NullParent();

		RemoveSocketFromNetwork(socket);

		UpdateChildrenFallback();
	}

	private void RemoveSocketFromNetwork(IExchangeSocket socket)
	{
		_logger.Log($"Removing {socket.GetEndpoint()} from network");

		if (socket.GetEndpoint() is not null)
			_groupSender.Remove(socket.GetEndpoint()!);

		_children.Remove(socket);
	}

	private void AddSocketToNetwork(IExchangeSocket socket)
	{
		_logger.Log($"Adding {socket.GetEndpoint()} to network");

		_groupListener.AddListener(socket);
		_groupSender.Add(socket);
	}

	private void AddChild(IExchangeSocket socket)
	{
		_logger.Log($"Adding child {socket.GetEndpoint()}");

		_children.Add(socket);
		AddSocketToNetwork(socket);
	}

	private void NullParent()
	{
		_logger.Log("Removing parent");

		_parentConnection = null;
		_parentListener = null;
	}

	private void StartListening()
	{
		_listenerThread.Start();
	}

	private void ListenWork()
	{
		_logger.Log("Begin listen");

		_listener.Start();

		while (_running)
		{
			var accepted = _listener.Accept();

			lock (_lock)
			{
				if (accepted is not null) AddChild(accepted);
			}
		}
	}

	private void SendMessage(string content)
	{
		_logger.Log($"Sending message {content}");

		var message = new UserMessage(content, _name);

		_groupSender.DuplicateMessage(message);

		_messages.Add(message);
	}

	private void SetParent(IPEndPoint address, IExchangeSocket socket)
	{
		_logger.Log($"Setting new parent {address}, {socket.GetEndpoint()}");

		_parentConnection = socket;
		_parentListener = address;
	}

	private void SendFallbackRequest()
	{
		_logger.Log("Requesting fallback");

		_parentConnection?.Send(new FallbackListenerRequest());
	}

	private void UpdateChildrenFallback()
	{
		_logger.Log("Updating fallback for children");

		foreach (var child in _children)
		{
			var childFallback = GetFallbackAddress(child);

			child.Send(childFallback);
		}
	}

	private IExchangeSocket ConnectTo(IPEndPoint address)
	{
		_logger.Log($"Connecting to {address}");

		return _client.Connect(address);
	}
}