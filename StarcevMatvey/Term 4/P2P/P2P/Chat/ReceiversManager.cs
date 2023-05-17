using P2P.Net;
using P2P.MessengeTypes;

namespace P2P.Chat
{
    public class ReceiversManager : IDisposable
    {
        private volatile bool _stop;
        private readonly object _lock;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private List<Thread> _receiversThreads;
        private List<Connect> _toClose;

        private ConnectionsManager _connectionsManager
        {
            get
            {
                return _connectionsManager;
            }
            set
            {
                if (!_initedConnectionManager)
                {
                    _connectionsManager = value;
                    _initedConnectionManager = true;
                }
            }
        }

        private bool _initedConnectionManager;
        private bool _disposed;

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder)
        {
            _stop = false;
            _lock = l;
            _disposed = false;
            _initedConnectionManager = false;
            _encoder = encoder;

            _receiversThreads = new List<Thread>();
            _toClose = new List<Connect>();
        }

        public void SetConnectionManager(ConnectionsManager manager)
        {
            _connectionsManager = manager;
        }

        public void Add(Connect con)
        {
            if (!_initedConnectionManager) throw new Exception("Connections manager for receivers are not setted");

            _toClose.Add(con);

            var th = new Thread(() => Receive(con));
            _receiversThreads.Add(th);
            th.Start();
        }

        private void Receive(Connect con)
        {
            while (!_stop)
            {
                var mes = con.Receive();

                if (mes.Type == MessengeTypes.TypeOfData.Empty) continue;

                lock (_lock)
                {
                    WorkWithData(mes);
                }
            }
        }

        private void WorkWithData(Messenge mes)
        {
            switch (mes.Type)
            {
                case TypeOfData.Listeners:
                    _connectionsManager.Merge(_encoder.GetConnections(mes));
                    break;
                case TypeOfData.RegularMessenge:
                    Console.WriteLine(mes.Data);
                    break;
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _stop = true;
            
            _toClose.ForEach(x => x.Dispose());
            _receiversThreads.ForEach(x => x.Join());
            _connectionsManager.Dispose();

            _disposed = true;
        }
    }
}
