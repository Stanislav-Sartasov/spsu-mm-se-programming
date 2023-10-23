using System.Net;
using System.Net.Sockets;
using System.Text;
using LocalChatter.Utils;

namespace LocalChatter
{
	public class ChatClient : IDisposable
	{
		private Socket _socket;
		private string _name;

		private volatile bool _disposed = false;
		private volatile bool _mainThreadStopped = true;

		private Action<ChatMessage> _forwarder;

		private Thread _mainThread;

		private Dictionary<string, EndPoint> _connectedClients = new();

		public ChatClient(string name, Action<ChatMessage> messageForwarder)
		{
			_name = name;
			_forwarder = messageForwarder;
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			_socket.Bind(NetworkHelper.GetEndPointFromName(name)!);

			_socket.ReceiveTimeout = Config.Timeout;
			_socket.SendTimeout = Config.Timeout;

			_socket.ReceiveBufferSize = Config.MessageSize;
			_socket.SendBufferSize = Config.MessageSize;

			_mainThread = new Thread(MainLoop);
		}

		public void Disconnect()
		{
			SendMessage(new ChatMessage(_name, MessageType.Leave), false);
			lock (_connectedClients) _connectedClients.Clear();
		}

		public bool ConnectToClient(string connectTo)
		{
			lock (_connectedClients)
			{
				if (_connectedClients.Count != 0) return false; // have to close current session first

				IPEndPoint? connectingClient = NetworkHelper.GetEndPointFromName(connectTo);
				if (connectingClient == null) return false; // incorrect name

				_connectedClients[connectTo] = connectingClient;
				var connectMessage = new ChatMessage(_name, MessageType.Connect);
				try
				{
					_socket.SendTo(Encoding.UTF8.GetBytes(connectMessage.Serialize()), connectingClient);
				}
				catch (SocketException ex) // failed to connect
				{
					_connectedClients.Remove(connectTo);
					return false;
				}
			}
			_forwarder(new ChatMessage(connectTo, MessageType.Connect));
			return true;
		}

		private void SendMessage(ChatMessage chatMessage, bool isForwarded = true)
		{
			if (isForwarded) _forwarder(chatMessage);
			byte[] serializedMessage = Encoding.UTF8.GetBytes(chatMessage.Serialize());

			lock (_connectedClients)
			{
				List<string> disconnectedClients = new();
				foreach (var client in _connectedClients)
				{
					try
					{
						_socket.SendTo(serializedMessage, client.Value);
					}
					catch (SocketException ex) // connection lost
					{
						disconnectedClients.Add(client.Key);
					}
				}
				foreach (var client in disconnectedClients)
				{
					DisconnectClient(client);
				}
			}
		}

		public void SendMessage(string content)
		{
			SendMessage(new ChatMessage(_name, MessageType.Message, content));
		}

		public void Start()
		{
			if (_disposed)
				throw new ObjectDisposedException("ChatClient is disposed upon Start call!");
			if (!_mainThreadStopped) // already running
				return;
			_mainThreadStopped = false;
			_mainThread.Start();
		}

		private void DisconnectClient(string name)
		{
			if (_connectedClients.ContainsKey(name))
			{
				_forwarder(new ChatMessage(name, MessageType.Leave));
				_connectedClients.Remove(name);
			}
		}

		private void AddNewClient(string name)
		{
			_connectedClients[name] = NetworkHelper.GetEndPointFromName(name)!;
		}

		private void JoinClient(string name)
		{
			if (_connectedClients.ContainsKey(name))
				return;

			_forwarder(new ChatMessage(name, MessageType.NewClientJoined));
			AddNewClient(name);

			// sending our info to the new client
			var myInfo = new ChatMessage(_name, MessageType.ConnectConfirmation);
			_socket.SendTo(Encoding.UTF8.GetBytes(myInfo.Serialize()), _connectedClients[name]);
		}

		private void MessageHandler(string message)
		{
			var chatMessage = ChatMessage.DeserializeMessage(message);
			if (chatMessage == null) return; // ignore

			if (chatMessage.Type == MessageType.Message)
			{
				_forwarder(chatMessage);
				return;
			}

			var newUsername = chatMessage.Username;
			switch (chatMessage.Type)
			{
				case MessageType.Leave:
					lock (_connectedClients) DisconnectClient(newUsername);
					break;
				case MessageType.NewClientJoined:
					lock (_connectedClients) JoinClient(newUsername);
					break;
				case MessageType.Connect:
					SendMessage(new ChatMessage(newUsername, MessageType.NewClientJoined)); // sending client to others
					lock (_connectedClients) AddNewClient(newUsername); // adding it on our side
					break;
				case MessageType.ConnectConfirmation:
					lock (_connectedClients) AddNewClient(newUsername);
					break;
			}
		}

		private void MainLoop()
		{
			var messageBuffer = new byte[Config.MessageSize];
			int size;
			string message;
			while (!_disposed)
			{
				try
				{
					size = _socket.Receive(messageBuffer);
					message = Encoding.UTF8.GetString(messageBuffer, 0, size);
					MessageHandler(message);
				}
				catch (SocketException ex) // received timeout
				{
					continue;
				}
			}
			_mainThreadStopped = true;
		}

		public void Dispose()
		{
			_disposed = true;
			_mainThread.Join();
			Disconnect();
			_socket.Close();
		}
	}
}