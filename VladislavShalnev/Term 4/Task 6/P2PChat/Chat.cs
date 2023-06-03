using System.Net;
using System.Net.Sockets;

namespace P2PChat;

public class Chat : IDisposable
{
	private volatile bool _isStopped;

	private readonly Socket _acceptor;

	private readonly Thread _acceptorThread;
	private readonly List<Thread> _listeners = new();
	private readonly Dictionary<IPEndPoint, Connection> _participants = new();

	// Locker for participants and OnEvent execution
	private readonly object _locker = new();

	public delegate void EventHandler(ChatEvent e, IPEndPoint sender, string? payload);

	public event EventHandler? OnEvent;

	public Chat(int port)
	{
		var endPoint = new IPEndPoint(IPAddress.Any, port);

		_acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		_acceptor.Bind(endPoint);
		_acceptor.Listen();

		_acceptorThread = new Thread(AcceptIncoming);
		_acceptorThread.Start();
	}

	private void AddToChat(IPEndPoint endPoint, Connection conn)
	{
		var listener = new Thread(() => Listen(endPoint, conn));
		_listeners.Add(listener);
		listener.Start();

		lock (_locker)
		{
			_participants.Add(endPoint, conn);
		}
	}

	private void AcceptIncoming()
	{
		while (!_isStopped)
		{
			try
			{
				var conn = new Connection(_acceptor.Accept());

				string port = conn.Receive();

				// Setting the right port
				var endPoint = IPEndPoint.Parse($"{conn.RemoteEndPoint!.Address}:{port}");

				lock (_locker)
				{
					conn.Send(_participants.Count > 0 ? string.Join(' ', _participants.Keys) : "NO");
				}

				AddToChat(endPoint, conn);


				OnEvent?.Invoke(ChatEvent.Connect, endPoint, null);
			}
			catch
			{
				if (!_isStopped) throw;
			}
		}
	}

	private void Listen(IPEndPoint endPoint, Connection conn)
	{
		while (!_isStopped)
		{
			try
			{
				string received = conn.Receive();
				lock (_locker)
				{
					OnEvent?.Invoke(ChatEvent.Message, endPoint, received);
				}
			}
			catch (SocketException e)
			{
				lock (_locker)
				{
					switch (e.SocketErrorCode)
					{
						case SocketError.ConnectionReset:
							_participants.Remove(endPoint);
							OnEvent?.Invoke(ChatEvent.Disconnect, endPoint, null);
							break;
						default:
							if (!_isStopped)
								OnEvent?.Invoke(ChatEvent.Error, endPoint, e.Message);
							break;
					}
				}

				break;
			}
		}
	}

	private string InnerConnect(int selfPort, IPEndPoint endPoint)
	{
		var conn = new Connection(endPoint);
		conn.Send(selfPort.ToString());
		var received = conn.Receive();
		AddToChat(endPoint, conn);

		return received;
	}

	public void Connect(IPEndPoint endPoint)
	{
		int selfPort = ((IPEndPoint) _acceptor.LocalEndPoint!).Port;

		lock (_locker)
		{
			if (_participants.Count > 0)
				throw new Exception("You're already connected to some chat");

			if (endPoint.Port == selfPort)
				throw new Exception("Can't connect to myself");

			if (_participants.Keys.Contains(endPoint))
				throw new Exception($"Already connected to {endPoint}");
		}

		string addresses = InnerConnect(selfPort, endPoint);

		if (addresses != "NO")
		{
			foreach (var address in addresses.Split())
				InnerConnect(selfPort, IPEndPoint.Parse(address));
		}

		OnEvent?.Invoke(ChatEvent.Connect, endPoint, null);
	}

	public void Send(string msg)
	{
		lock (_locker)
		{
			OnEvent?.Invoke(ChatEvent.Message, (IPEndPoint) _acceptor.LocalEndPoint!, msg);

			foreach (var conn in _participants.Values)
				conn.Send(msg);
		}
	}

	public void Dispose()
	{
		_isStopped = true;

		_acceptor.Close();
		_acceptorThread.Join();

		foreach (var conn in _participants.Values)
			conn.Close();

		foreach (var listener in _listeners)
			listener.Join();
	}
}