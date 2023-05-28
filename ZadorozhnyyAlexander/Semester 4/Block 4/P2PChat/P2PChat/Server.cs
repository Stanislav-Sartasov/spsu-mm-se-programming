using System.Net;
using System.Net.Sockets;

namespace P2PChat
{
    public class Server : IDisposable
    {
        private string address;
        private int port;
        private Thread me;

        private Socket socket;

        public IPEndPoint TCPEndPoint;

        public string Address => address;

        public int Port => port;

        public Server(string address, int port)
        {
            this.address = address;
            this.port = port;
            TCPEndPoint = IPEndPoint.Parse($"{address}:{port}");

            socket = new Socket(TCPEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(TCPEndPoint);
            socket.Listen(10);
        }

        public void Activate(ThreadStart threadStart)
        {
            me = new Thread(threadStart);
            me.Start();
        }

        public SocketHelper? Accept()
        {
            try { return new SocketHelper(socket.Accept()); }
            catch { return null; }
        }

        public void Dispose()
        {
            socket.Close();
            me.Join();
        }
    }
}
