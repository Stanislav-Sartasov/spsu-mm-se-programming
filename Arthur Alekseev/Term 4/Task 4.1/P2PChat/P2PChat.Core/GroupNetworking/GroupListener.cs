using System.Net;
using System.Net.Sockets;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.GroupNetworking;

/// <summary>
///     Class for listening a group of sockets
/// </summary>
public class GroupListener
{
	public delegate void ErrorDelegate(IExchangeSocket socket, SocketException exception);

	public delegate void MessageDelegate(IExchangeSocket socket, string message);

	private readonly Dictionary<IPEndPoint, Thread> _listenerThreads;
	private readonly object _lock;
	private readonly Dictionary<IPEndPoint, bool> _runningThreads;
	private readonly List<IExchangeSocket> _sockets;

	public GroupListener(object locker)
	{
		_listenerThreads = new Dictionary<IPEndPoint, Thread>();
		_runningThreads = new Dictionary<IPEndPoint, bool>();
		_lock = locker;
		_sockets = new List<IExchangeSocket>();
	}

	/// <summary>
	///     Event to handle message from one of the sockets
	///     ip - IP of the socket
	///     socket - socket instance
	///     message - message string
	/// </summary>
	public event MessageDelegate? OnMessageReceive;

	/// <summary>
	///     Event to handle error from one of the sockets
	///     ip - IP of the socket
	///     socket - socket instance
	///     exception - socket exception
	/// </summary>
	public event ErrorDelegate? OnSocketError;

	/// <summary>
	///     Adds listener to a list and starts the thread
	/// </summary>
	/// <param name="sender"></param>
	public void AddListener(IExchangeSocket sender)
	{
		var ip = sender.GetEndpoint()!;
		var thread = new Thread(() => ListenForMessage(ip, sender));
		_listenerThreads.Add(ip, thread);
		_runningThreads.Add(ip, true);
		_sockets.Add(sender);
		thread.Start();
	}

	private void ListenForMessage(IPEndPoint ip, IExchangeSocket socket)
	{
		while (_runningThreads[ip])
			try
			{
				var msg = socket.Receive();

				lock (_lock)
				{
					OnMessageReceive?.Invoke(socket, msg);
				}
			}
			catch (SocketException exception)
			{
				lock (_lock)
				{
					if (_runningThreads[ip])
						RemoveAndCall(ip, socket, exception);
				}
			}
			catch (AggregateException ae)
			{
				lock (_lock)
				{
					if (ae.InnerException is SocketException exception && _runningThreads[ip])
						RemoveAndCall(ip, socket, exception);
				}
			}
	}

	private void RemoveAndCall(IPEndPoint ip, IExchangeSocket socket, SocketException exception)
	{
		_runningThreads[ip] = false;
		_sockets.Remove(socket);
		OnSocketError?.Invoke(socket, exception);

	}

	/// <summary>
	///     Stop all threads
	/// </summary>
	public void Join()
	{
		foreach (var runningThreadKey in _runningThreads.Keys)
			_runningThreads[runningThreadKey] = false;

		foreach (var socket in _sockets)
			socket.Stop();

		foreach (var key in _listenerThreads.Keys)
			_listenerThreads[key].Join();
	}
}