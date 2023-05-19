using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using P2P.MessengeTypes;
using P2P.MessengeEncoder;
using P2P.Loggers;

namespace P2P.Net
{
    public class Connect : IDisposable
    {
        private readonly Socket _socket;
        private readonly MessengeEncoder.MessengeEncoder _encoder;

        public ILogger Logger { get; }

        public Connect(IPEndPoint peerEndPoint, ILogger logger)
        {
            Logger = logger;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(peerEndPoint);

            _encoder = new MessengeEncoder.MessengeEncoder();
        }

        public Connect (Socket socket, ILogger logger)
        {
            Logger = logger;

            _socket = socket;
            _encoder = new MessengeEncoder.MessengeEncoder();
        }

        public Connect(Socket socket, MessengeEncoder.MessengeEncoder encoder, ILogger logger)
        {
            Logger = logger;
            _socket = socket;
            _encoder = encoder;
        }

        public Connect(IPEndPoint peerEndPoint)
        {
            Logger = new Logger();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(peerEndPoint);

            _encoder = new MessengeEncoder.MessengeEncoder();
        }

        public Connect(Socket socket)
        {
            Logger = new Logger();

            _socket = socket;
            _encoder = new MessengeEncoder.MessengeEncoder();
        }

        public Connect(Socket socket, MessengeEncoder.MessengeEncoder encoder)
        {
            Logger = new Logger();
            _socket = socket;
            _encoder = encoder;
        }

        public Connect WithSocket(Socket socket) => new Connect(socket, _encoder, Logger);

        public Connect WithEncoder(MessengeEncoder.MessengeEncoder encoder) => new Connect(_socket, encoder, Logger);

        public Connect WithLogger(ILogger logger) => new Connect(_socket, _encoder, logger);

        public IPEndPoint LocalEndPoint => (IPEndPoint)_socket.LocalEndPoint;
        public IPEndPoint RemoteEndPoint => (IPEndPoint)_socket.RemoteEndPoint;

        public void Send(Messenge messenge)
        {
            Logger.Log($"I am sending \"{messenge.Data}\"\nReshuffle {messenge.Union}  Type {messenge.Type}");

            var mes = _encoder.ToMessenge(messenge);

            if (_socket.Send(mes) <= 0) throw new Exception("I could't send your messenge T.T");
        }

        public Messenge Receive()
        {
            byte[] mes = new byte[_encoder.MaxMessengeLength];
            var getted = _socket.Receive(mes);

            if (getted <= 0) throw new Exception("I can't get yout messenge >.<");

            var messenge = _encoder.FromMessenge(mes);

            Logger.Log($"I get your messenge \"{messenge.Data}\"");

            return messenge;
        }

        public void SendEmpty() => Send(Messenge.Empty);

        public void SendNoUnion() => Send(Messenge.NoUnoion);

        public void SendUnion() => Send(Messenge.YesUnion);

        public void Close()
        {
            _socket.Close();
        }

        public void Dispose()
        {
            Close();
            _socket.Dispose();
            Logger.Log($"Connect {_socket} was disposed");
        }
    }
}
