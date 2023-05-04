using System.Net;

namespace P2PChat.Core.Networking.Interfaces;

/// <summary>
///		Socket used for accepting incoming connection connections
/// </summary>
public interface IListenerSocket : IDisposable
{
	/// <summary>
	///		Start listening for incoming connections
	/// </summary>
	public void Start();

	/// <summary>
	///		Accept new connection and return it as a IExchangeSocket
	/// </summary>
	/// <returns>Connected IExchangeSocket</returns>
	public IExchangeSocket? Accept();

	/// <summary>
	///		Get remote endpoint
	/// </summary>
	/// <returns>Remote endpoint</returns>
	public IPEndPoint GetEndpoint();

	/// <summary>
	///		Stops listening to the incoming connections
	/// </summary>
	public void Cancel();
}