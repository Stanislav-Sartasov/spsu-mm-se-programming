using System.Net;
using P2PChat.Core.Json;
using P2PChat.Core.Networking.Interfaces;

namespace P2PChat.Core.GroupNetworking;

/// <summary>
///     Class for spreading messages through the sockets
/// </summary>
public class GroupSender
{
	private readonly object _lock;
	private readonly Dictionary<IPEndPoint, IExchangeSocket> _neighborSockets;

	public GroupSender(object locker)
	{
		_neighborSockets = new Dictionary<IPEndPoint, IExchangeSocket>();
		_lock = locker;
	}

	/// <summary>
	///     Adds object to the target list
	/// </summary>
	/// <param name="sender">socket</param>
	public void Add(IExchangeSocket sender)
	{
		lock (_lock)
		{
			_neighborSockets.Add(sender.GetEndpoint()!, sender);
		}
	}

	/// <summary>
	///     Removes objects from the target list
	/// </summary>
	/// <param name="ip">IP to remove</param>
	public void Remove(IPEndPoint ip)
	{
		lock (_lock)
		{
			_neighborSockets.Remove(ip);
		}
	}

	/// <summary>
	///     Sends message to all targets Added, except for target with IP equal to ipToIgnore
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="msg">message to be spread</param>
	/// <param name="ipToIgnore">IP to ignore</param>
	public void DuplicateMessage<T>(IJsonObject<T> msg, IPEndPoint ipToIgnore)
	{
		lock (_lock)
		{
			foreach (var socket in _neighborSockets.Where(socket => !Equals(socket.Key, ipToIgnore)))
				socket.Value.Send(msg);
		}
	}

	/// <summary>
	///     Sends message to all targets Added
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="msg">message to be spread</param>
	public void DuplicateMessage<T>(IJsonObject<T> msg)
	{
		lock (_lock)
		{
			foreach (var socket in _neighborSockets)
				socket.Value.Send(msg);
		}
	}
}