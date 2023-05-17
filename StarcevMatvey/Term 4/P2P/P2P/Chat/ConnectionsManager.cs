using P2P.MessengeTypes;
using P2P.Net;
using System.Net;
using System.Net.Sockets;

namespace P2P.Chat
{
    public class ConnectionsManager : IDisposable
    {
        private readonly int _port;
        private readonly ReceiversManager _receiversManager;

        private Dictionary<IPEndPoint, Connect> _connections;

        private bool _disposed;

        public ConnectionsManager(int port, ReceiversManager receiversManager)
        {
            _port = port;
            _receiversManager = receiversManager;
            _disposed = false;

            _connections = new Dictionary<IPEndPoint, Connect>();
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

                var c = new Connect(con);
                c.SendNoUnion();

                _receiversManager.Add(c);
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

        public List<IPEndPoint> ToConnections(Messenge mes)
        {
            var strs = mes.Data.Split().ToList();
            var rez = new List<IPEndPoint>();

            strs.ForEach(x => rez.Add(IPEndPoint.Parse(x)));

            return rez;
        }

        public void Dispose()
        {
            if (_disposed) return;

            _receiversManager.Dispose();

            _disposed = true;
        }
    }
}
