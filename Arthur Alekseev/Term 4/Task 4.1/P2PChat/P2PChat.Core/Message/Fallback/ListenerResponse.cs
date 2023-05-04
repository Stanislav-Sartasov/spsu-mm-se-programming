using System.Net;
using Newtonsoft.Json;
using P2PChat.Core.Json;
using P2PChat.Core.Message.Wrapper;

namespace P2PChat.Core.Message.Fallback;

/// <summary>
///     Message containing IP address of a _listener
///     HasFallback - true if there is address or else otherwise
///     ListenerEndpoint - IP Address of _listener or 0.0.0.0:0 if HasFallback is false
/// </summary>
public class ListenerResponse : AJsonObject<ListenerResponse>
{
	public ListenerResponse(bool hasFallback, IPEndPoint listenerEndpoint)
	{
		HasFallback = hasFallback;
		ListenerEndpoint = new IpEndPointWrapper(listenerEndpoint);
	}

	[JsonConstructor]
	public ListenerResponse(bool hasFallback, IpEndPointWrapper listenerEndpoint)
	{
		HasFallback = hasFallback;
		ListenerEndpoint = listenerEndpoint;
	}

	public bool HasFallback { get; }

	public IpEndPointWrapper ListenerEndpoint { get; }
}