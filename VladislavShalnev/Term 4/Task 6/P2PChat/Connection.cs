using System.Net;
using System.Net.Sockets;
using System.Text;

namespace P2PChat;

public class Connection
{
	private readonly Socket _socket;

	public Connection(Socket socket)
	{
		_socket = socket;
	}

	public Connection(IPEndPoint endPoint)
	{
		_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		_socket.Connect(endPoint);
	}

	public void Send(string msg) => _socket.Send(Encoding.UTF8.GetBytes(msg));

	public string Receive()
	{
		var buffer = new byte[256];
		var result = new StringBuilder();

		do
		{
			int size = _socket.Receive(buffer);
			result.Append(Encoding.UTF8.GetString(buffer, 0, size));
		} while (_socket.Available > 0);

		return result.ToString();
	}

	public IPEndPoint? RemoteEndPoint => (IPEndPoint?) _socket.RemoteEndPoint;

	public void Close() => _socket.Close();
}