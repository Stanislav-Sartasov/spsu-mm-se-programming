using P2P.Net;
using System.Net;
using P2P.MessengeTypes;
using P2P.MessengeEncoder;
using P2P.Loggers;

namespace P2P.Chat
{
    public class Client : IDisposable
    {
        private volatile bool _stop;

        private readonly int _port;

        private readonly Listener _listener;
        private readonly Thread _listenerThread;

        public readonly ReceiversManager ReceiversManager;
        private readonly ConnectionsManager _connectionsManager;

        private readonly MessengeEncoder.MessengeEncoder _encoder = new MessengeEncoder.MessengeEncoder();

        private readonly object _lock = new object();

        public delegate void MessengeEvent(string messenge);
        public event MessengeEvent MessengeSend;

        public ILogger Logger { get; }

        public Client(int port, ILogger logger)
        {
            _port = port;
            _listener = new Listener(port);

            Logger = logger;

            ReceiversManager = new ReceiversManager(_lock, _encoder, Logger);
            _connectionsManager = new ConnectionsManager(_port, ReceiversManager, Logger);

            _listenerThread = new Thread(Listen);
            _listenerThread.Start();
        }

        public Client(int port)
        {
            _port = port;
            _listener = new Listener(port);

            Logger = new Logger();

            ReceiversManager = new ReceiversManager(_lock, _encoder, Logger);
            _connectionsManager = new ConnectionsManager(_port, ReceiversManager, Logger);

            _listenerThread = new Thread(Listen);
            _listenerThread.Start();
        }

        private void Listen()
        {
            Logger.Log("I am start listen");

            while (!_stop)
            {
                var conect = _listener.Accept().WithEncoder(_encoder);

                lock (_lock)
                {
                    var mes = conect.Receive();
                    conect.SendEmpty();

                    if (mes.Union == MessengeTypes.Union.NoUnion)
                    {
                        ReceiversManager.Add(conect, _connectionsManager);
                        Logger.Log($"I added new connection");
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

                        ReceiversManager.Add(conect, _connectionsManager);

                        Logger.Log($"I union conections of to conect");
                    }
                }
            }
        }

        public void Conect(IPEndPoint adrs)
        {
            Logger.Log($"I am connecting to {adrs}");

            if (_connectionsManager.Contains(adrs))
            {
                Logger.Log($"I already connected to {adrs}");
                return;
            }

            if (adrs.Port == _port)
            {
                Logger.Log($"I can not connect to myself");
                return;
            }

            var con = new Connect(adrs, Logger);

            con.SendUnion();
            con.Receive();

            con.Send(new Messenge(_port.ToString(), Union.NoUnion, TypeOfData.Listeners));
            con.Receive();

            _connectionsManager.Add(adrs, con);

            con.Send(_connectionsManager.ToMessenge());
            con.Receive();

            ReceiversManager.Add(con, _connectionsManager);

            Logger.Log($"I am connected to {adrs}");
        }

        public void Send(string data)
        {
            var mes = new Messenge(data, Union.NoUnion, TypeOfData.RegularMessenge);

            _connectionsManager.SendToAll(mes);

            Logger.Log($"I sended to all conections {data}");
            if(MessengeSend != null) MessengeSend.Invoke(data);
        }

        public void Dispose()
        {
            _stop = true;
            _listener.Dispose();
            _listenerThread.Join();

            _connectionsManager.Dispose();
            ReceiversManager.Dispose();

            Logger.Log($"I disposed client with post {_port}");
        }
    }
}
