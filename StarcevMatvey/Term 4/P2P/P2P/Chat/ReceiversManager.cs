using P2P.Net;
using P2P.MessengeTypes;
using P2P.Loggers;

namespace P2P.Chat
{
    public class ReceiversManager : IDisposable
    {
        private volatile bool _stop;
        private readonly object _lock;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private List<Thread> _receiversThreads;
        private List<Connect> _toClose;

        private bool _disposed;

        public ILogger Logger { get; }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder, ILogger logger)
        {
            _stop = false;
            _lock = l;
            _disposed = false;
            _encoder = encoder;
            Logger = logger;

            _receiversThreads = new List<Thread>();
            _toClose = new List<Connect>();
        }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder)
        {
            _stop = false;
            _lock = l;
            _disposed = false;
            _encoder = encoder;
            Logger = new Logger();

            _receiversThreads = new List<Thread>();
            _toClose = new List<Connect>();
        }

        public void Add(Connect con, ConnectionsManager manager)
        {
            _toClose.Add(con);

            var th = new Thread(() => Receive(con, manager));
            _receiversThreads.Add(th);
            th.Start();
        }

        private void Receive(Connect con, ConnectionsManager manager)
        {
            while (!_stop)
            {
                var mes = con.Receive();

                if (mes.Type == MessengeTypes.TypeOfData.Empty) continue;

                lock (_lock)
                {
                    WorkWithData(mes, manager);
                }
            }
        }

        private void WorkWithData(Messenge mes, ConnectionsManager manager)
        {
            switch (mes.Type)
            {
                case TypeOfData.Listeners:
                    manager.Merge(_encoder.GetConnections(mes));
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

            _disposed = true;

            Logger.Log($"I disposed receivers manager");
        }
    }
}
