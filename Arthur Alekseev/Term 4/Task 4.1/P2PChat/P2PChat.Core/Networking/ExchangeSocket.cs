using System.Net;
using System.Net.Sockets;
using System.Text;
using P2PChat.Core.Json;
using P2PChat.Core.Log;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.Networking;

public class ExchangeSocket : IExchangeSocket
{
	private readonly ILogger _logger;
	private readonly Socket _socket;
	private readonly Queue<string> _receiveBuffer;
	private Task<int>? _receiveTask;

	public ExchangeSocket(Socket socket, ILogger logger)
	{
		_socket = socket;
		_logger = logger;
		_receiveBuffer = new Queue<string>();
	}

	public void Dispose()
	{
		_socket.Shutdown(SocketShutdown.Both);
		_socket.Close();
		_socket.Dispose();
	}

	public IPEndPoint? GetEndpoint()
	{
		try
		{
			return _socket.RemoteEndPoint as IPEndPoint;
		}
		catch
		{
			return null;
		}
	}

	public IPEndPoint? GetLocalEndpoint()
	{
		return _socket.LocalEndPoint as IPEndPoint;
	}

	public void Send<T>(IJsonObject<T> message)
	{
		var msg = Encoding.UTF8.GetBytes((char)0x01 + message.ToJson());
		_logger.Log($"-----------\nSending {message} to {GetEndpoint()}");
		try
		{
			_socket.Send(msg);
		}
		catch
		{
			// ignored
		}
	}

	public string Receive()
	{
		if (_receiveBuffer.Count > 0)
			return _receiveBuffer.Dequeue();

		var buffer = new byte[4096];
		_receiveTask = _socket.ReceiveAsync(buffer);

		var length = _receiveTask.Result;

		if (length == 0)
			throw new SocketException();

		var responses = Encoding.UTF8.GetString(buffer, 0, length).Split((char)0x01).Skip(1);

		foreach (var responsePart in responses)
		{
			_receiveBuffer.Enqueue(responsePart);
			_logger.Log($"-----------\nReceived {responsePart} from {GetEndpoint()}");
		}

		return _receiveBuffer.Count > 0 ? _receiveBuffer.Dequeue() : "";
	}

	public void Wait()
	{
		_receiveTask?.Wait();
	}

	public void Stop()
	{
		try
		{
			_socket.Close();
		}
		catch
		{
			// ignore
		}
	}
}