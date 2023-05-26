using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Task6.Network
{
    public class Client
    {
        private const string _joinCommand = "JOIN_";
        private const string _receiveClients = "RLIST_";
        private const string _sendClients = "SLIST_";
        private const string _closeConnection = "CLOSECONN_";
        private const string _message = "MESSAGE_";

        private IReceiver _receiver;

        private Listener _listener;
        private List<Connection> _senders;
        private List<Connection> _receivers;
        private volatile List<Thread> _receiverThreads;
        private Thread listenThread;
        private volatile int run = 1;

        public Client(int port, IReceiver receiver)
        {
            _listener = new Listener(port);
            _senders = new List<Connection>();
            _receivers = new List<Connection>();
            _receiver = receiver;
            _receiverThreads = new List<Thread>();
            listenThread = new Thread(Receive);

            listenThread.Start();
        }

        public void Connect(IPAddress ipAddr, int port)
        {
            Connection conn = new Connection(ipAddr, port);
            conn.Send(String.Format("{0}{1}", _joinCommand, _listener.LocalEndPoint.ToString()));
            lock (_senders)
            {
                _senders.Add(conn);
            }
            string status = conn.Receive();
            lock (_receiver)
            {
                _receiver.Connect(conn.RemoteEndPoint, status);
            }
            
            RequestClients(conn);
        }

        private void RequestClients(Connection conn)
        {
            conn.Send(_receiveClients);
            string cliensList = conn.Receive();
            cliensList = cliensList.Replace(_sendClients, "");
            string[] cliens = cliensList.Split(",");
            for (int i = 0; i < cliens.Length; i++)
            {
                IPAddress clientIP = ParseIPAddr("", cliens[i]);
                int clientPort = ParsePort("", cliens[i]);

                if (!IsMe(clientIP, clientPort) && !Connected(clientIP, clientPort))
                {
                    Connect(clientIP, clientPort);
                }
            }
        }

        public void Send(string text)
        {
            lock (_receiver)
            {
                _receiver.Send(text);
            }
            lock (_senders)
            {
                for (int i = 0; i < _senders.Count; i++)
                {
                    if (_senders[i].Connected)
                    {
                        _senders[i].Send(String.Format("{0}{1}", _message, text));
                    }
                }
            }
        }

        private void Receive()
        {
            while (true)
            {
                Connection rec = _listener.Accept();
                if (0 == Interlocked.CompareExchange(ref run, 0, 0))
                    break;
                if (rec != null)
                {
                    string text = rec.Receive();
                    if (text.Contains(_joinCommand))
                    {
                        _receivers.Add(rec);
                        Thread thread = new Thread(() => ReceiveAction(rec, text));
                        thread.Start();
                        _receiverThreads.Add(thread);
                    }
                }
            }
        }

        private void ReceiveAction(Connection connection, string text)
        {
            IPAddress ipAddr = ParseIPAddr(_joinCommand, text);
            int port = ParsePort(_joinCommand, text);

            if (!Connected(ipAddr, port))
                Connect(ipAddr, port);

            connection.Send("Y");

            while (0 != Interlocked.CompareExchange(ref run, 0, 0))
            {
                if (!connection.Connected)
                {
                    break;
                }
                text = connection.Receive();
                if (text.Contains(_message))
                {
                    text = text.Replace(_message, "");
                    lock(_receiver)
                    {
                        _receiver.Receive(connection.RemoteIP, text);
                    }
                }
                else if (text.Contains(_receiveClients))
                {
                    connection.Send(String.Format("{0}{1}", _sendClients, _connectedList));
                }
                else if (text.Contains(_closeConnection))
                {
                    break;
                }
            }
            connection.Close();
        }

        public void Stop()
        {
            Interlocked.Exchange(ref run, 0);

            lock (_senders)
            {
                for (int i = 0; i < _senders.Count; i++)
                {
                    _senders[i].Send(_closeConnection);
                    _senders[i].Close();
                }
                _senders.Clear();
            }
            lock (_receivers)
            {
                for (int i = 0; i < _receivers.Count; i++)
                {
                    _receivers[i].Close();
                }
                _receivers.Clear();
            }

            lock (_receiverThreads)
            {
                for (int i = 0; i < _receiverThreads.Count; i++)
                {
                    _receiverThreads[i].Join();
                }
                _receivers.Clear();
            }

            Connection conn = new Connection(_listener.LocalIP, _listener.LocalPort);
            conn.Send("Y");
            listenThread.Join();
            conn.Close();
            _listener.Close();
        }

        public IPAddress IPAddr
        {
            get
            {
                return _listener.LocalIP;
            }
        }

        public int Port
        {
            get
            {
                return _listener.LocalPort;
            }
        }

        private string _connectedList
        {
            get
            {
                string connectedList = "";
                lock (_senders)
                {
                    for (int i = 0; i < _senders.Count; i++)
                    {
                        if (_senders[i].Connected)
                        {
                            if (i > 0)
                                connectedList += String.Format(",{0}:{1}", _senders[i].RemoteIP, _senders[i].RemotePort);
                            else
                                connectedList += String.Format("{0}:{1}", _senders[i].RemoteIP, _senders[i].RemotePort);
                        }
                    }
                }
                return connectedList;
            }
        }

        private bool IsMe(IPAddress ipAddr, int port)
        {
            return _listener.LocalPort == port && _listener.LocalIP.Equals(ipAddr);
        }

        private bool Connected(IPAddress ipAddr, int port)
        {
            bool connected = false;
            for (int i = 0; i < _senders.Count; i++)
            {
                connected |= (_senders[i].RemotePort == port && _senders[i].RemoteIP.Equals(ipAddr));
            }
            return connected;
        }

        private int ParsePort(string command, string text)
        {
            string endPoint = text;
            if (command.Length > 0)
                endPoint = text.Replace(command, "");
            var addr = endPoint.Split(":");
            int port = int.Parse(addr[1]);
            return port;
        }

        private IPAddress ParseIPAddr(string command, string text)
        {
            string endPoint = text;
            if (command.Length > 0)
                endPoint = text.Replace(command, "");
            var addr = endPoint.Split(":");
            IPAddress ipAddr = IPAddress.Parse(addr[0]);
            return ipAddr;
        }
    }
}
