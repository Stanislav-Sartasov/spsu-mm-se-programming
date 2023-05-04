using System.Net;
using P2PChat.Core.Json;

namespace P2PChat.Core.Networking.Interfaces;

/// <summary>
///		Socket used for communication, receiving and sending messages
/// </summary>
public interface IExchangeSocket : IDisposable
{
	/// <summary>
	///		Gets Remote endpoint
	/// </summary>
	/// <returns>Remote endpoint</returns>
	public IPEndPoint? GetEndpoint();

	/// <summary>
	///		Gets Local endpoint
	/// </summary>
	/// <returns>Local endpoint</returns>
	public IPEndPoint? GetLocalEndpoint();

	/// <summary>
	///		Sends IJsonObject
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="message">message</param>
	public void Send<T>(IJsonObject<T> message);

	/// <summary>
	///		Gets string that was sent
	/// </summary>
	/// <returns></returns>
	public string Receive();

	/// <summary>
	///		Waits until Receive happens
	/// </summary>
	public void Wait();

	/// <summary>
	///		Stop socket
	/// </summary>
	public void Stop();
}