using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace PeerToPeerChat.Network
{
    public class Listener
    {
        private Socket socket;
        public IPEndPoint Bound;

        public Listener(int port)
        {
            var localEndPoint = new IPEndPoint(IPAddress.Any, port);

            socket = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Bound = localEndPoint;

            socket.Bind(localEndPoint);
            socket.Listen(10);
        }

        public Connection? Accept()
        {
	        try
	        {
		        var res = socket.Accept();

		        return new Connection(res);
	        }
	        catch
	        {
		        return null;
	        }

        }

        public void Close()
        {
			socket.Close();
        }
    }
}
