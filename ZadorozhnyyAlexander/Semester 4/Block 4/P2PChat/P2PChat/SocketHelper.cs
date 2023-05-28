using System.Text;
using System.Net.Sockets;
using System.Net;

namespace P2PChat
{
    public class SocketHelper : IDisposable
    {
        private Socket socket;
        private Exception socketError = new Exception("Socket already closed");

        public SocketHelper(IPEndPoint address)
        {
            socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(address);
        }

        public SocketHelper(Socket s) { socket = s; }

        public IPEndPoint GetLocalEndPoint() => (IPEndPoint)socket.LocalEndPoint!;

        public IPEndPoint GetRemoteEndPoint() => (IPEndPoint)socket.RemoteEndPoint!;

        public void Send(string data)
        {
            try
            {
                byte[] message = Encoding.UTF8.GetBytes(data);
                int byteSent = socket.Send(message);

                if (byteSent < 0)
                    throw socketError;

                Console.WriteLine($"[INFO] {data} sended.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ERROR] {e.Message}");
            }
        }

        public string Receive()
        {
            var buffer = new byte[1024];
            var size = socket.Receive(buffer);

            if (size == 0)
                throw socketError;

            var data = Encoding.UTF8.GetString(buffer, 0, size);

            Console.WriteLine($"[INFO] {data} received.");

            return data;
        }

        public void Dispose() { socket.Close(); }
    }
}
