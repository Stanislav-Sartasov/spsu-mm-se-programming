using System.Net;
using System.Net.Sockets;

namespace P2PChat
{
    public class Peer : IDisposable
    {
        private volatile bool isStop;
        private object obj = new object();
        private string delimiter = ";";

        private Server server;

        private Dictionary<IPEndPoint, SocketHelper?> connections = new Dictionary<IPEndPoint, SocketHelper?>();
        private List<SocketHelper> sockets = new List<SocketHelper>();
        private List<Thread> receivers = new List<Thread>();

        public delegate void WpfEvent(string message);
        public event WpfEvent OnWpf;

        public Peer(string address, int port) { server = new Server(address, port); }

        public void Activate() { server.Activate(GetAllConns); }

        public void Send(string message)
        {
            foreach (var conn in connections.Keys)
                connections[conn]?.Send($"M{delimiter}{server.Address}: {message}");

            OnWpf?.Invoke($"me: {message}");
        }

        public void Connect(IPEndPoint address)
        {
            Console.WriteLine($"[INFO] Connecting to {address}...");

            if (!CheckConn(address))
            {
                Console.WriteLine($"Address {address} is not valid");
                return;
            }

            // Connect to address
            var socketHelper = new SocketHelper(address);

            Console.WriteLine($"[INFO] Connected to {address}...");

            Update(socketHelper, address);
        }

        private bool CheckConn(IPEndPoint address) => !connections.ContainsKey(address) && server.Port != address.Port;

        private void Update(SocketHelper socketHelper, IPEndPoint address)
        {
            // Send necessery data
            SAR(socketHelper, "Y");
            SAR(socketHelper, server.Port.ToString());

            connections.Add(address, socketHelper);

            // Send all connections to other
            SAR(socketHelper, string.Join(delimiter, connections.Keys.ToList()));

            AddReceiver(socketHelper);
        }

        private void SAR(SocketHelper socketHelper, string m) // Send-And-Receive
        {
            socketHelper.Send(m);
            socketHelper.Receive();
        }

        private void AddReceiver(SocketHelper? conn)
        {
            if (conn == null)
                return;

            var thread = new Thread(() => Receive(conn));

            sockets.Add(conn);
            receivers.Add(thread);

            thread.Start();
        }

        private void Receive(SocketHelper conn)
        {
            while (!isStop)
            {
                try
                {
                    lock (obj) { ReceiveHelper(conn.Receive()); }
                }
                catch { break; }
            }
        }

        private void ReceiveHelper(string message)
        {
            if (message == "")
                return;

            var m = message.Split(delimiter).ToList();
            var cmd = m[0];
            var args = m.Skip(1).ToList();

            switch (cmd)
            {
                case "L":
                    Concatenate(ConvertToIpEndpoint(args));
                    break;

                case "M":
                    OnWpf?.Invoke(string.Join(delimiter, args));
                    break;
            }
        }

        private void Concatenate(List<IPEndPoint> endpoints) // Concatenate new List of endPoints and curr
        {
            foreach (var endpoint in endpoints.Where(x => !connections.ContainsKey(x)).ToList())
            {
                if (endpoint.Address.AddressFamily != AddressFamily.InterNetwork || endpoint.Port != server.Port)
                {
                    var conn = new SocketHelper(endpoint);
                    conn.Send("N");

                    // Start receiving messages
                    AddReceiver(conn);
                    connections.Add(endpoint, conn);
                }
                else
                    connections.Add(endpoint, null);
            }
        }

        private List<IPEndPoint> ConvertToIpEndpoint(List<string> strings) => strings.Select(x => ConvertToIpEndpoint(x)).ToList().Where(x => x != null).ToList();

        private IPEndPoint ConvertToIpEndpoint(string s)
        {
            if (IPEndPoint.TryParse(s, out var endPoint))
                return endPoint;
            return null;
        }

        private void GetAllConns()
        {
            while (!isStop)
            {
                SocketHelper? conn = server.Accept();

                if (conn == null)
                    continue;

                lock (obj)
                {

                    // Check do we need handshake
                    var isHandShake = conn.Receive();
                    conn.Send("_");

                    // If no, just listen
                    if (isHandShake == "N")
                    {
                        AddReceiver(conn);
                        continue;
                    }

                    DoHandShake(conn);
                }
            }
        }

        private void DoHandShake(SocketHelper conn)
        {
            if (!int.TryParse(conn.Receive(), out var port))
                return;

            conn.Send("_");

            var address = new IPEndPoint(conn.GetRemoteEndPoint().Address, port);

            if (!connections.ContainsKey(address))
                connections.Add(address, conn);

            // Take list of connections and concatenate it
            var conns = conn.Receive();
            conn.Send("_");
            Concatenate(ConvertToIpEndpoint(conns.Split(delimiter).ToList()));

            // Update state
            SendConns();
            AddReceiver(conn);
        }

        // Send list of all listeners to other
        private void SendConns()
        {
            connections.Values.ToList().ForEach(connection => connection?.Send($"L{delimiter}{string.Join(delimiter, connections.Keys.ToList())}"));
        }

        public void Dispose()
        {
            isStop = true;

            sockets.ForEach(x => x?.Dispose());

            server.Dispose();
            receivers.Where(x => x.ThreadState == ThreadState.Running).ToList().ForEach(x => x.Join());

            Console.WriteLine("[INFO] Stopped");
        }
    }
}