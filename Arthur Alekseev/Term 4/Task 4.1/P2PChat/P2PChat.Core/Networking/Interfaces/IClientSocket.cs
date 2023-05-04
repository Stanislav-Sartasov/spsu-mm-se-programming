using System.Net;

namespace P2PChat.Core.Networking.Interfaces;

/// <summary>
///		IClientSocket is used to create new IExchangeSocket connected to endpoint
/// </summary>
public interface IClientSocket
{
	/// <summary>
	///		Create new IExchangeSocket connected to endpoint
	/// </summary>
	/// <param name="endpoint">Endpoint of another listener</param>
	/// <returns>connected IExchange socket</returns>
	public IExchangeSocket Connect(IPEndPoint endpoint);
}