using PeerToPeerChat.Network;
using System.Net;
using System.Net.Sockets;

namespace PeerToPeerChat.Chat
{
	public class ChatClient : IDisposable
	{
		// Socket accepting connections
		private Listener listener;

		// Port listener is working on
		private int port;

		// IP -> Connection (table of all connected users with their listener IP)
		private Dictionary<IPEndPoint, Connection?> connections;

		private List<Connection> socketsToClose;

		// Dispose var
		private volatile bool stopped;

		// Receive lock
		private object receivelock = new();

		// List of receiver threads
		private List<Thread> receivers;

		// Listener thread
		private Thread listenerThread;

		// Message event
		public delegate void OnMessageEvent(string message);
		public event OnMessageEvent OnMessage;

		public ChatClient(int port)
		{
			connections = new();
			this.port = port;
			listener = new Listener(port);
			socketsToClose = new();
			receivers = new List<Thread>();

			listenerThread = new Thread(AcceptConnections);
			listenerThread.Start();
		}

		private void AddReceiver(Connection? conn)
		{
			if (conn is null)
				return;

			socketsToClose.Add(conn);

			var thread = new Thread(() => ReceiveFrom(conn));

			receivers.Add(thread);

			thread.Start();
		}

		private void AcceptConnections()
		{
			Console.WriteLine("Started accepting loop");
			while (!stopped)
			{
				// Accept connection
				var connection = listener.Accept();

				if (connection is null)
					continue;

				lock (receivelock)
				{
					Console.WriteLine("Accepted connection");

					// Needs standard handshake (actions below)?
					var needsHandshake = connection.Receive();
					connection.Send("EMPTY");

					// If no handshake needed just listen
					if (needsHandshake == "N")
					{
						AddReceiver(connection);
						continue;
					}

					// Ask for listener port
					var port = int.Parse(connection.Receive());
					connection.Send("EMPTY");

					// Add to connections
					var address = new IPEndPoint(connection.RemoteAddress().Address, port);

					if (!connections.ContainsKey(address))
						connections.Add(address, connection);

					// Ask for connection list
					var connectionConnections = connection.Receive();
					connection.Send("EMPTY");

					// Merge connections
					MergeConnections(CreateEndpoints(connectionConnections.Split().ToList()));

					// Send update dictionary
					SendConnections();

					// Listen to messages
					AddReceiver(connection);
				}
			}
		}

		// Creates endpoint list from string list
		private List<IPEndPoint> CreateEndpoints(List<string> strings)
		{
			var endpoints = new List<IPEndPoint>();

			foreach (var endpointString in strings)
			{
				var parsed = IPEndPoint.TryParse(endpointString, out var result);
				if (parsed) endpoints.Add(result!);
			}

			return endpoints;
		}


		// Merges a new List of endpoints and current dict, connects to new endpoints
		private void MergeConnections(List<IPEndPoint> endpoints)
		{
			foreach (var endpoint in endpoints)
			{
				// If not me and not connected
				if (CheckEndpoint(endpoint))
				{
					// Connect
					var connection = new Connection(endpoint);

					// Refuse handshake
					connection.Send("N");

					// Start receiving messages
					AddReceiver(connection);

					// Add to connections
					connections.Add(endpoint, connection);

					continue;
				}

				// If is not valid but not contained
				if (!connections.ContainsKey(endpoint))
				{
					// Adding null in order to send the address to others
					connections.Add(endpoint, null);
				}
			}
		}

		// Do we need to add the endpoint?
		private bool CheckEndpoint(IPEndPoint endPoint)
		{
			// Is self?
			if (endPoint.Address.AddressFamily == AddressFamily.InterNetwork && endPoint.Port == port)
				return false;

			// Is connected?
			if (connections.ContainsKey(endPoint))
				return false;

			return true;
		}

		private void SendConnections()
		{
			// Get list of listeners
			var message = connections.Keys.ToList();

			// Send to every address
			foreach (var connection in connections.Values)
				connection?.Send("LISTENERS " + string.Join(" ", message));
		}


		private void ReceiveFrom(Connection conn)
		{
			// Receive while app is alive
			while (!stopped)
			{
				try
				{
					// Receive data
					var data = conn.Receive();

					if (data == "")
						continue;

					lock (receivelock)
					{
						// Dispatch data
						OnReceive(data);
					}
				}
				catch
				{
					break;
				}
			}
		}

		private void OnReceive(string message)
		{
			var t = message.Split()[0];
			var m = string.Join(" ", message.Split().Skip(1));

			switch (t)
			{
				case "LISTENERS":
					var endpoints = CreateEndpoints(m.Split().ToList());
					MergeConnections(endpoints);
					break;

				case "MESSAGE":
					Console.WriteLine(m);
					OnMessage?.Invoke(m);
					break;
			}
		}

		public void ConnectTo(IPEndPoint address)
		{
			Console.WriteLine("Joining " + address);

			if (connections.ContainsKey(address))
			{
				Console.WriteLine("Already joined");
				return;
			}

			if (address.Port == port)
			{
				Console.WriteLine("Cannot join myself");
				return;
			}

			// Connect to address
			var conn = new Connection(address);

			Console.WriteLine("Joined");

			// Send yes to handshake
			conn.Send("Y");
			conn.Receive();

			// Send listener port
			conn.Send(port.ToString());
			conn.Receive();

			// Add new connection
			connections.Add(address, conn);

			// Send connections
			var message = connections.Keys.ToList();
			conn.Send(string.Join(" ", message));
			conn.Receive();

			// Listen to messages
			AddReceiver(conn);
		}

		public void Send(string message)
		{

			foreach (var conn in connections.Keys)
			{
				connections[conn]?.Send("MESSAGE " + message);
			}

			OnMessage?.Invoke(message);
		}

		public void Dispose()
		{
			stopped = true;

			// Destroy listening threads
			listener.Close();

			// Destroy receiving threads
			foreach (var connection in socketsToClose)
				connection?.Close();

			// Join all threads
			listenerThread.Join();

			foreach (var thread in receivers)
				if (thread.ThreadState == ThreadState.Running)
					thread.Join();

			Console.WriteLine("Disposed");
		}
	}
}
