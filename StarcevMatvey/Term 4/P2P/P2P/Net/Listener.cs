using System.Net;
using System.Net.Sockets;
using P2P.Loggers;

namespace P2P.Net
{
    public class Listener : IDisposable
    {
        private readonly Socket _socket;
        private readonly IPEndPoint _peerEndPoint;

        public ILogger Logger { get; }

        public Listener(int port, ILogger logger)
        {
            Logger = logger;
            _peerEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Start();
        }

        public Listener(Socket socket, IPEndPoint peerEndPoint, ILogger logger)
        {
            Logger = logger;
            _socket = socket;
            _peerEndPoint = peerEndPoint;

            Start();
        }

        public Listener(int port)
        {
            Logger = new Logger();
            _peerEndPoint = new IPEndPoint(IPAddress.Any, port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Start();
        }

        public Listener(Socket socket, IPEndPoint peerEndPoint)
        {
            Logger = new Logger();
            _socket = socket;
            _peerEndPoint = peerEndPoint;

            Start();
        }

        public Listener WithSocket(Socket socket) => new Listener(socket, _peerEndPoint, Logger);

        public Listener WithEndpoint(IPEndPoint peerEndPoint) => new Listener(_socket, peerEndPoint, Logger);

        public Listener WithLogger(ILogger logger) => new Listener(_socket, _peerEndPoint, logger);

        public void Start()
        {
            Logger.Log($"I started listening {_socket}");
            _socket.Bind(_peerEndPoint);
            _socket.Listen(1000);
        }

        public Connect Accept() => new Connect(_socket.Accept(), Logger);

        public void Close()
        {
            Logger.Log($"I closed {_socket}");
            _socket.Close();
        }

        public void Dispose()
        {
            Close();
            _socket.Dispose();
            Logger.Log($"I disposed {_socket}");
        }
    }
}
