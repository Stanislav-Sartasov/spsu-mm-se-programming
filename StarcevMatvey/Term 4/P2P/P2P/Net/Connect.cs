using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using P2P.MessengeTypes;
using P2P.MessengeEncoder;

namespace P2P.Net
{
    public class Connect : IDisposable
    {
        private readonly Socket _socket;
        private readonly MessengeEncoder.MessengeEncoder _encoder;

        public Connect(IPEndPoint peerEndPoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(peerEndPoint);

            _encoder = new MessengeEncoder.MessengeEncoder();
        }

        public Connect(Socket socket, MessengeEncoder.MessengeEncoder encoder)
        {
            _socket = socket;
            _encoder = encoder;
        }

        public Connect WithSocket(Socket socket) => new Connect(socket, _encoder);
        public Connect WithEncoder(MessengeEncoder.MessengeEncoder encoder) => new Connect(_socket, encoder);

        public IPEndPoint LocalEndPoint => (IPEndPoint)_socket.LocalEndPoint;
        public IPEndPoint RemoteEndPoint => (IPEndPoint)_socket.RemoteEndPoint;

        public void Send(Messenge messenge)
        {
            Console.WriteLine($"I am sending \"{messenge.Data}\"\nReshuffle {messenge.Reshuffle}  Type {messenge.Type}");

            var mes = _encoder.ToMessenge(messenge);

            if (_socket.Send(mes) <= 0) throw new Exception("I could't send your messenge T.T");
        }

        public Messenge Receive()
        {
            byte[] mes = new byte[_encoder.MaxMessengeLength];
            var getted = _socket.Receive(mes);

            if (getted <= 0) throw new Exception("I can't get yout messenge >.<");

            var messenge = _encoder.FromMessenge(mes);

            Console.WriteLine($"I get your messenge \"{messenge.Data}\"");

            return messenge;
        }

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
