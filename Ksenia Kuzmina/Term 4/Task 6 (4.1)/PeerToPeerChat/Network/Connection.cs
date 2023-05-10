using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Net.WebSockets;

namespace PeerToPeerChat.Network
{
    public class Connection
    {
        private Socket socket;

        public Connection(IPEndPoint address)
        {
            socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(address);
        }

        public Connection(Socket socket)
        {
            this.socket = socket;
        }

        public void Send(string data)
        {
            try
            {
                Console.WriteLine("Sending: " + data);

                byte[] messageSent = Encoding.UTF8.GetBytes(data);
                int byteSent = socket.Send(messageSent);

                if (byteSent < 0) throw new Exception("Remote socket closed");
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }

        public string Receive()
        {
            var messageReceived = new byte[1024];
            var byteRecv = socket.Receive(messageReceived);
            var res = Encoding.UTF8.GetString(messageReceived, 0, byteRecv);

            if (byteRecv == 0)
	            throw new Exception("Remote socket closed");
            Console.WriteLine("Received " + res);

            return res;
        }

        public IPEndPoint LocalAddress()
        {
            return socket.LocalEndPoint as IPEndPoint;
        }

        public IPEndPoint RemoteAddress()
        {
            return socket.RemoteEndPoint as IPEndPoint;
        }

        public void Close()
        {
            socket.Close();
        }
    }
}
