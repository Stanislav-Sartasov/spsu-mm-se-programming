using System.Net;
using System.Net.Sockets;

namespace LocalChatter.Utils
{
	public static class NetworkHelper
	{
		public static string? GetCurrentMachineName()
		{
			var ip = GetHostLocalIP();
			if (ip == null) return null;

			var port = GetFreeAddressPort(ip);
			if (port == null) return null;

			return $"{ip}:{port}";
		}

		public static IPAddress? GetHostLocalIP()
		{
			IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

			var ipAddress = host
				.AddressList
				.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);

			if (ipAddress == null) return null;
			return ipAddress;
		}

		public static int? GetFreeAddressPort(IPAddress address)
		{
			int port;
			var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			var localEp = new IPEndPoint(address, 0);
			try
			{
				socket.Bind(localEp);
				localEp = (IPEndPoint)socket.LocalEndPoint!;
				port = localEp.Port;
			}
			catch
			{
				return null;
			}
			finally
			{
				socket.Close();
			}
			return port;
		}

		public static IPEndPoint? GetEndPointFromName(string name)
		{
			var ipAndPort = name.Split(':');
			if (ipAndPort.Length != 2) return null;

			IPAddress? ip;
			if (!IPAddress.TryParse(ipAndPort[0], out ip)) return null;

			int port;
			if (!int.TryParse(ipAndPort[1], out port) || ip == null) return null;

			return new IPEndPoint(ip, port);
		}
	}
}
