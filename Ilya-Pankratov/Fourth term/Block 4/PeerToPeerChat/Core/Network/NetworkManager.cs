using System.Net;
using System.Net.Sockets;

namespace Core.Network;

public static class NetworkManager
{
    public static IPAddress GetLocalIp()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        return host.AddressList.First(x => x.AddressFamily == AddressFamily.InterNetwork);
    }

    public static IPAddress GetLocalHostIp()
    {
        return Dns.GetHostEntry("localhost").AddressList.First();
    }
}