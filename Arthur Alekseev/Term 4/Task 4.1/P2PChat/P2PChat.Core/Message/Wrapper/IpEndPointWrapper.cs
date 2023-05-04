using System.Net;
using Newtonsoft.Json;

namespace P2PChat.Core.Message.Wrapper;

/// <summary>
///     Json-friendly wrapper for IPEndPoint
/// </summary>
public class IpEndPointWrapper
{
	// ReSharper disable all
	[JsonIgnore] public IPEndPoint Endpoint;

	[JsonProperty] private string address;

	[JsonProperty] private int port;
	// ReSharper enable all

	public IpEndPointWrapper(IPEndPoint endpoint)
	{
		Endpoint = endpoint;
		address = endpoint.Address.ToString();
		port = endpoint.Port;
	}

	[JsonConstructor]
	public IpEndPointWrapper(string address, int port)
	{
		Endpoint = IPEndPoint.Parse($"{address}:{port}");
		this.address = address;
		this.port = port;
	}
}