using System.Net;
using System.Net.Sockets;

namespace P2P.Net
{
    public class Listener : IDisposable
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _peerEndPoint;

        public Listener(int port)
        {
            _peerEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public Listener(Socket socket, IPEndPoint peerEndPoint)
        {
            _socket = socket;
            _peerEndPoint = peerEndPoint;
        }

        public Listener WithSocket(Socket socket) => new Listener(socket, _peerEndPoint);

        public Listener WithEndpoint(IPEndPoint peerEndPoint) => new Listener(_socket, peerEndPoint);

        public void Start()
        {
            _socket.Bind(_peerEndPoint);
            _socket.Listen(1000);
        }

        public Connect Accept() => new Connect(_socket.Accept());

        public void Close()
        {
            _socket.Close();
        }

        public void Dispose()
        {
            Close();
            _socket.Dispose();
        }
    }
}
