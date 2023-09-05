using System.Net;

namespace P2P_Chat_App.Model
{
    internal class CurrentUserModel
    {
        public string Name { get; set; }
        public int LocalPort { get; set; }
        public IPAddress LocalIpAddress { get; set; }
        public IPAddress RemoteIpAddress { get; set; }
        public int RemotePort { get; set; }
        public bool IsConnected { get; set; }
    }
}
