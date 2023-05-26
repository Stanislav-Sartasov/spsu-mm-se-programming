using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Network
{
    public class Listener
    {
        private Socket _socket;

        public Listener(int port)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[1];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);
            _socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _socket.Bind(localEndPoint);
            _socket.Listen(100);
        }

        public Connection Accept()
        {
            Socket res = _socket.Accept();

            return new Connection(res);
        }

        public EndPoint LocalEndPoint
        {
            get
            {
                return _socket.LocalEndPoint;
            }
        }

        public int LocalPort
        {
            get
            {
                var addr = LocalEndPoint.ToString().Split(":");
                int port = int.Parse(addr[1]);
                return port;
            }
        }

        public IPAddress LocalIP
        {
            get
            {
                var addr = LocalEndPoint.ToString().Split(":");
                IPAddress ipAddr = IPAddress.Parse(addr[0]);
                return ipAddr;
            }
        }

        public void Close()
        {
            _socket.Close();
        }
    }
}
