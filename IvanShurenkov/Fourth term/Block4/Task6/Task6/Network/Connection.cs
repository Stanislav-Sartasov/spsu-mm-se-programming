using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Network
{
    public class Connection
    {
        private Socket _socket;
        private bool socketClosed = false;

        private bool socketConnected
        {
            get
            {
                return _socket.Connected && !(_socket.Poll(1000, SelectMode.SelectRead) && (_socket.Available == 0));
            }
        }
        public bool Connected
        {
            get
            {
                return _socket != null && !socketClosed && socketConnected;
            }
        }

        public Connection(IPAddress ipAddr, int port)
        {
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);
            try
            {
                _socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.Connect(localEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public Connection(Socket socket)
        {
            _socket = socket;
        }

        public int Send(string text)
        {
            text += "\n";
            byte[] messageSent = Encoding.ASCII.GetBytes(text);
            try
            {
                if (!Connected)
                {
                    return 0;
                }
                int cntByte = _socket.Send(messageSent);
                return cntByte;
            }
            catch (Exception e)
            {
                socketClosed = true;
                Console.WriteLine(e.ToString());
                return 0;
            }
        }

        public string Receive()
        {
            string text = String.Empty;
            while (true)
            {
                byte[] message = new byte[1024];
                if (!Connected)
                {
                    return String.Empty;
                }
                try
                {
                    int cntByte = _socket.Receive(message);
                    text += Encoding.ASCII.GetString(message, 0, cntByte);

                    if (cntByte <= 0 || text.Contains("\n"))
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    socketClosed = true;
                    Console.WriteLine(e.ToString());
                }
            }
            return text;
        }

        public EndPoint RemoteEndPoint
        {
            get
            {
                return _socket.RemoteEndPoint;
            }
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

        public int RemotePort
        {
            get
            {
                var addr = RemoteEndPoint.ToString().Split(":");
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

        public IPAddress RemoteIP
        {
            get
            {
                 var addr = RemoteEndPoint.ToString().Split(":");
                IPAddress ipAddr = IPAddress.Parse(addr[0]);
                return ipAddr;
            }
        }

        public void Close()
        {
            if (Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
                socketClosed = true;
            }
        }
    }
}
