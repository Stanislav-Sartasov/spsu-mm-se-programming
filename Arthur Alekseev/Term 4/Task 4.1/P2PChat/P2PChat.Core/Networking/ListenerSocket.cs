using System.Net;
using P2PChat.Core.Log;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.Networking;

public class ListenerSocket : ASocket, IListenerSocket
{
	private readonly IPEndPoint _localEndPoint;
	private readonly ILogger _logger;

	public ListenerSocket(int port, ILogger logger)
	{
		_localEndPoint = new IPEndPoint(IPAddress.Any, port);
		_logger = logger;
	}

	public void Start()
	{
		Socket.Bind(_localEndPoint);
		Socket.Listen(100);
	}

	public IExchangeSocket? Accept()
	{
		try
		{
			var accepted = Socket.Accept();
			_logger.Log($"Accepted connection from: {accepted.RemoteEndPoint}");
			return new ExchangeSocket(accepted, _logger);
		}
		catch 
		{
			return null;
		}
	}

	public void Cancel()
	{
		try
		{
			_logger.Log($"Stopping socket: {Socket.LocalEndPoint}");
			Socket.Close();
		}
		catch
		{
			// ignore
		}
	}

	public void Dispose()
	{
		Cancel();
		Socket.Dispose();
	}
}