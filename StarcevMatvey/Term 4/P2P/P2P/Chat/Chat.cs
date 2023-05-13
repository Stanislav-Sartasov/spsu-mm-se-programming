using P2P.Net;
using System.Net;

namespace P2P.Chat
{
    public class Chat : IDisposable
    {
        private volatile bool _stop;

        private int _port;

        private Listener _listener;
        private Thread _listenerThread;

        private List<Thread> _receiversThreads;
        private List<Connect> _toClose;
        private Dictionary<IPEndPoint, Connect> _connections;

        private object _lock;

        public Chat(int port)
        {
            _port = port;
            _listener = new Listener(port);
            _receiversThreads = new List<Thread>();
            _toClose = new List<Connect>();
            _connections = new Dictionary<IPEndPoint, Connect>();

            _listenerThread = new Thread(() => throw new Exception("not implemented"));
            _listenerThread.Start();
        }

        public void Dispose()
        {
            _stop = true;
            _listener.Dispose();
            _listenerThread.Join();
            _toClose.ForEach(x => x.Dispose());
            _receiversThreads.ForEach(x => x.Join());
        }
    }
}
