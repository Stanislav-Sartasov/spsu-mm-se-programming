using System.Net;
using System.Net.Sockets;

namespace Core.Network;

public static class NetworkManager
{
    public static IPAddress GetLocalIp()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        var ip = host.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
        return ip;
    }

    public static IPAddress GetLocalHostIp()
    {
        return Dns.GetHostEntry("localhost").AddressList.First();
    }

    public static bool IsPortValid(int port)
    {
        return 0 < port && port < 65536;
    }
}