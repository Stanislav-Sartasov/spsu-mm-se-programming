using System.Net;
using System.Net.Sockets;

namespace P2PChat.Core.Networking;

public class FreePortFinder
{
	/// <summary>
	/// Find free port
	/// </summary>
	/// <returns>Port that is free to use</returns>
	public static int FindFreePort()
	{
		int port;
		var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			var localEp = new IPEndPoint(IPAddress.Any, 0);
			socket.Bind(localEp);
			localEp = (IPEndPoint)socket.LocalEndPoint!;
			port = localEp.Port;
		}
		finally
		{
			socket.Close();
		}

		return port;
	}
}