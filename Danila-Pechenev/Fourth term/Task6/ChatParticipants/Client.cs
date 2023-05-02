using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;

namespace ChatParticipants
{
    public sealed class Client : AParticipant
    {
        public override string IP { get; protected set; }

        public override int Port { get; protected set; }

        private readonly Dictionary<string, EndPoint> clientEndPoints = new Dictionary<string, EndPoint>();
        private readonly Socket socket;
        private readonly EndPoint serverEndPoint;
        private Thread listenThread;
        private readonly Action<string> callback;
        private volatile bool stopped = false;
        private readonly object lockObject = new object();

        public Client(string ip, int port, Action<string> callback)
        {
            IP = ip;
            Port = port;
            this.callback = callback;

            var myEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(myEndPoint);

            serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.ServerIP), Constants.ServerPort);
            socket.SendTo(Encoding.UTF8.GetBytes($"JOIN;{ip};{port}"), serverEndPoint);
        }

        public override void Start()  // Create a new chat
        {
            listenThread = new Thread(Listen);
            listenThread.Start();
        }

        public void Start(string participantIP, int participantPort)  // Join an existing chat
        {
            var participantEndPoint = new IPEndPoint(IPAddress.Parse(participantIP), participantPort);
            socket.SendTo(Encoding.UTF8.GetBytes($"JOIN;{IP};{Port}"), participantEndPoint);

            clientEndPoints.Add($"{participantIP};{participantPort}", participantEndPoint);

            Start();
        }
        
        public void SendMessage(string message)
        {
            callback(message);

            foreach (var item in clientEndPoints)
            {
                socket.SendTo(Encoding.UTF8.GetBytes($"MESSAGE;{IP};{Port};{message}"), item.Value);
            }
        }

        public override void Dispose()
        {
            lock(lockObject)
            {
                stopped = true;
            }

            var endPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            var helper = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            helper.Connect(endPoint);
            helper.Send(Encoding.UTF8.GetBytes("STOP"));

            listenThread.Join();

            socket.SendTo(Encoding.UTF8.GetBytes($"LEAVE;{IP};{Port}"), serverEndPoint);
            foreach (var item in clientEndPoints)
            {
                socket.SendTo(Encoding.UTF8.GetBytes($"LEAVE;{IP};{Port}"), item.Value);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            helper.Shutdown(SocketShutdown.Both);
            helper.Close();

            clientEndPoints.Clear();
        }

        protected override void Listen()
        {
            var buffer = new byte[Constants.MessageBufferSize];
            int size;
            string message;
            string address;
            char[] delimiterChars = { ';' };
            IPEndPoint endPoint;
            while (true)
            {
                lock(lockObject)
                {
                    if (stopped)
                    {
                        return;
                    }
                }

                size = socket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, size);

                string[] words = message.Split(delimiterChars, 4);
                try
                {
                    address = $"{words[1]};{words[2]}";
                }
                catch
                {
                    continue;
                }

                if (words[0] == "JOIN")
                {
                    endPoint = new IPEndPoint(IPAddress.Parse(words[1]), int.Parse(words[2]));
                    clientEndPoints.Add(address, endPoint);
                }
                else if (words[0] == "LEAVE")
                {
                    if (clientEndPoints.ContainsKey(address))
                    {
                        clientEndPoints.Remove(address);
                    }
                }
                else if (words[0] == "MESSAGE")
                {
                    callback(words[3]);

                    foreach (var item in clientEndPoints)
                    {
                        if (item.Key == address)
                        {
                            continue;
                        }

                        socket.SendTo(Encoding.UTF8.GetBytes($"MESSAGE;{IP};{Port};{words[3]}"), item.Value);
                    }
                }
                else
                {
                    // Ignore message
                }
            }
        }
    }
}
