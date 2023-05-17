using P2P.Net;
using System.Net;
using P2P.MessengeTypes;
using P2P.MessengeEncoder;

namespace P2P.Chat
{
    public class Client : IDisposable
    {
        private volatile bool _stop;

        private readonly int _port;

        private readonly Listener _listener;
        private readonly Thread _listenerThread;

        private readonly ReceiversManager _receiversManager;
        private readonly ConnectionsManager _connectionsManager;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private readonly object _lock;

        public Client(int port)
        {
            _port = port;
            _listener = new Listener(port);
            _lock = new object();

            _encoder = new MessengeEncoder.MessengeEncoder();
            _receiversManager = new ReceiversManager(_lock, _encoder);
            _connectionsManager = new ConnectionsManager(_port, _receiversManager);
            _receiversManager.SetConnectionManager(_connectionsManager);

            _listenerThread = new Thread(Listen);
            _listenerThread.Start();
        }

        private void Listen()
        {
            Console.WriteLine("I am start listen");

            while (!_stop)
            {
                var conect = _listener.Accept().WithEncoder(_encoder);

                lock (_lock)
                {
                    var mes = conect.Receive();
                    conect.SendEmpty();

                    if (mes.Union == MessengeTypes.Union.NoUnion)
                    {
                        _receiversManager.Add(conect);
                        continue;
                    }

                    if (mes.Union == MessengeTypes.Union.Union)
                    {
                        var port = _encoder.GetPort(conect.Receive());
                        conect.SendEmpty();

                        var adrs = new IPEndPoint(conect.RemoteEndPoint.Address, port);
                        _connectionsManager.AddIfNotExist(adrs, conect);

                        var cons = _encoder.GetConnections(conect.Receive());
                        conect.SendEmpty();

                        _connectionsManager.Merge(cons);

                        _connectionsManager.SendToAll(_connectionsManager.ToMessenge());

                        _receiversManager.Add(conect);
                    }
                }
            }
        }

        public void Conect(IPEndPoint adrs)
        {
            Console.WriteLine($"I am connecting to {adrs}");

            if (_connectionsManager.Contains(adrs))
            {
                Console.WriteLine($"I already connected to {adrs}");
                return;
            }

            if (adrs.Port == _port)
            {
                Console.WriteLine($"I can not connect to myself");
                return;
            }

            var con = new Connect(adrs);

            con.SendUnion();
            con.Receive();

            con.Send(new Messenge(_port.ToString(), Union.NoUnion, TypeOfData.Listeners));
            con.Receive();

            _connectionsManager.Add(adrs, con);

            con.Send(_connectionsManager.ToMessenge());
            con.Receive();

            _receiversManager.Add(con);
        }

        public void Send(string data)
        {
            var mes = new Messenge(data, Union.NoUnion, TypeOfData.RegularMessenge);

            _connectionsManager.SendToAll(mes);
        }

        public void Dispose()
        {
            _stop = true;
            _listener.Dispose();
            _listenerThread.Join();

            _connectionsManager.Dispose();
            _receiversManager.Dispose();
        }
    }
}
