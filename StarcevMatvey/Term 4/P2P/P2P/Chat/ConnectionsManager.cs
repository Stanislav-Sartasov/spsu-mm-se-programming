using P2P.MessengeTypes;
using P2P.Net;
using System.Net;
using System.Net.Sockets;
using P2P.Loggers;

namespace P2P.Chat
{
    public class ConnectionsManager : IDisposable
    {
        private readonly int _port;
        private readonly ReceiversManager _receiversManager;

        private Dictionary<IPEndPoint, Connect> _connections = new Dictionary<IPEndPoint, Connect>();

        private ILogger Logger { get; }

        public ConnectionsManager(int port, ReceiversManager receiversManager, ILogger logger)
        {
            _port = port;
            _receiversManager = receiversManager;
            Logger = logger;
        }

        public ConnectionsManager(int port, ReceiversManager receiversManager)
        {
            _port = port;
            _receiversManager = receiversManager;
            Logger = new Logger();
        }

        public bool Contains(IPEndPoint key) => _connections.ContainsKey(key);

        public void Add(IPEndPoint key, Connect v) => _connections.Add(key, v);

        public void AddIfNotExist(IPEndPoint key, Connect v)
        {
            if (!Contains(key)) Add(key, v);
        }

        public void Remove(IPEndPoint key) => _connections.Remove(key);

        public void RemoveIfExist(IPEndPoint key)
        {
            if (Contains(key)) Remove(key);
        }

        public void Merge(List<IPEndPoint> cons)
        {
            foreach (var con in cons)
            {
                if (con.Address.AddressFamily == AddressFamily.InterNetwork && con.Port == _port) continue;
                if (Contains(con)) continue;

                var c = new Connect(con, Logger);
                c.SendNoUnion();

                _receiversManager.Add(c, this);
                _connections.Add(con, c);
            }
        }

        public void SendToAll(Messenge mes)
        {
            foreach (var con in _connections.Keys)
            {
                _connections[con].Send(mes);
            }
        }

        public Messenge ToMessenge()
        {
            var data = string.Join(" ", _connections.Keys.ToList());

            return new Messenge(data, Union.NoUnion, TypeOfData.Listeners);
        }

        public void Dispose() => _receiversManager.Dispose();
    }
}
