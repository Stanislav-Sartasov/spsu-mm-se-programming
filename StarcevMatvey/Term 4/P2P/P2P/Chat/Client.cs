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

        private readonly ReceiversManager _receiversManager;
        private readonly ConnectionsManager _connectionsManager;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private readonly object _lock;

        public Invokes Invoke
        {
            get => this.Invoke;
            set
            {
                if (_invokeUpdt)
                {
                    this.Invoke = value;
                    _invokeUpdt = false;
                }
            }
        }
        private bool _invokeUpdt;

        public ILogger Logger { get; }

        public Client(int port, ILogger logger)
        {
            _port = port;
            _listener = new Listener(port);
            _lock = new object();
            _invokeUpdt = true;

            Logger = logger;

            _encoder = new MessengeEncoder.MessengeEncoder();
            _receiversManager = new ReceiversManager(_lock, _encoder, Logger);
            _connectionsManager = new ConnectionsManager(_port, _receiversManager, Logger);

            _listenerThread = new Thread(Listen);
            _listenerThread.Start();
        }

        public Client(int port)
        {
            _port = port;
            _listener = new Listener(port);
            _lock = new object();
            _invokeUpdt = true;

            Logger = new Logger();

            _encoder = new MessengeEncoder.MessengeEncoder();
            _receiversManager = new ReceiversManager(_lock, _encoder, Logger);
            _connectionsManager = new ConnectionsManager(_port, _receiversManager, Logger);

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
                        _receiversManager.Add(conect, _connectionsManager);
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

                        _receiversManager.Add(conect, _connectionsManager);

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

            _receiversManager.Add(con, _connectionsManager);

            Logger.Log($"I am connected to {adrs}");
        }

        public void Send(string data)
        {
            var mes = new Messenge(data, Union.NoUnion, TypeOfData.RegularMessenge);

            _connectionsManager.SendToAll(mes);

            Logger.Log($"I sended to all conections {data}");
            if(!_invokeUpdt) Invoke.Invoke(data);
        }

        public void Dispose()
        {
            _stop = true;
            _listener.Dispose();
            _listenerThread.Join();

            _connectionsManager.Dispose();
            _receiversManager.Dispose();

            Logger.Log($"I disposed client with post {_port}");
        }
    }
}
