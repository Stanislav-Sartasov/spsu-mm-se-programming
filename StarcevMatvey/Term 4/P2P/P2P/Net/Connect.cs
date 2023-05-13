using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2P.Net
{
    public class Connect : IDisposable
    {
        const int MESSENGE_LENGTH = 1024; // bytes

        private readonly Socket _socket;

        public Connect(IPEndPoint peerEndPoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(peerEndPoint);
        }

        public Connect(Socket socket)
        {
            _socket = socket;
        }

        public Connect WithSocket(Socket socket) => new Connect(socket);

        public IPEndPoint LocalEndPoint => (IPEndPoint)_socket.LocalEndPoint;
        public IPEndPoint RemoteEndPoint => (IPEndPoint)_socket.RemoteEndPoint;

        public void Send(string messenge)
        {
            Console.WriteLine($"I am sending \"{messenge}\"");

            var mes = Encoding.UTF8.GetBytes(messenge);
            mes = mes.Length <= MESSENGE_LENGTH ? mes : mes.Take(MESSENGE_LENGTH).ToArray();

            if (_socket.Send(mes) <= 0) throw new Exception("I could't send your messenge T.T");
        }

        public string Receive()
        {
            byte[] mes = new byte[MESSENGE_LENGTH];
            var getted = _socket.Receive(mes);

            if (getted <= 0) throw new Exception("I can't get yout messenge >.<");

            var messenge = Encoding.UTF8.GetString(mes, 0, getted);
            Console.WriteLine($"I get your messenge \"{messenge}\"");

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
