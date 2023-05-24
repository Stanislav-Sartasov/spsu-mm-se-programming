using P2P.Net;
using P2P.MessengeTypes;
using P2P.Loggers;

namespace P2P.Chat
{
    public class ReceiversManager : IDisposable
    {
        private volatile bool _stop = false;
        private readonly object _lock;

        private readonly MessengeEncoder.MessengeEncoder _encoder;

        private List<Thread> _receiversThreads = new List<Thread> ();
        private List<Connect> _toClose = new List<Connect> (); 

        private bool _disposed = false;

        public delegate void MessengeEvent(string messenge);
        public event MessengeEvent MessengeInvoke;

        public ILogger Logger { get; }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder, ILogger logger)
        {
            _lock = l;
            _encoder = encoder;
            Logger = logger;
        }

        public ReceiversManager(object l, MessengeEncoder.MessengeEncoder encoder)
        {
            _lock = l;
            _encoder = encoder;
            Logger = new Logger();
        }

        public void Add(Connect con, ConnectionsManager manager, MessengeEvent me)
        {
            _toClose.Add(con);

            var th = new Thread(() => Receive(con, manager, me));
            _receiversThreads.Add(th);
            th.Start();
        }

        private void Receive(Connect con, ConnectionsManager manager, MessengeEvent me)
        {
            while (!_stop)
            {
                var mes = con.Receive();

                if (mes.Type == MessengeTypes.TypeOfData.Empty) continue;

                lock (_lock)
                {
                    WorkWithData(mes, manager, me);
                }
            }
        }

        private void WorkWithData(Messenge mes, ConnectionsManager manager, MessengeEvent me)
        {
            switch (mes.Type)
            {
                case TypeOfData.Listeners:
                    manager.Merge(_encoder.GetConnections(mes));
                    break;
                case TypeOfData.RegularMessenge:
                    Console.WriteLine(mes.Data);
                    if (me != null) me.Invoke(mes.Data);
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
