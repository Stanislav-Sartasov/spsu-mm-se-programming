using System.Net;
using System.Net.Sockets;

namespace P2PChat.Core.Networking;

public abstract class ASocket
{
	protected readonly Socket Socket;

	protected ASocket()
	{
		Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}

	public IPEndPoint GetEndpoint()
	{
		return Socket.RemoteEndPoint as IPEndPoint;
	}
}