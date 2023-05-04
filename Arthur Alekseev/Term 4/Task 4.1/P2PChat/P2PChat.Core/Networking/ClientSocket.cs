using System.Net;
using System.Net.Sockets;
using P2PChat.Core.Log;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.Networking;

public class ClientSocket : IClientSocket
{
	private readonly ILogger _logger;

	public ClientSocket(ILogger logger)
	{
		_logger = logger;
	}

	public IExchangeSocket Connect(IPEndPoint endpoint)
	{
		var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(endpoint);

		_logger.Log($"Created connection: {socket.LocalEndPoint} -> {socket.RemoteEndPoint}");
		return new ExchangeSocket(socket, _logger);
	}
}