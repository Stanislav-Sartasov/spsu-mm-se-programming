using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatParticipants
{
    public sealed class Server : AParticipant
    {
        public override string IP { get; protected set; }

        public override int Port { get; protected set; }

        private readonly HashSet<string> adresses = new HashSet<string>();
        private readonly Socket socket;
        private Thread listenThread;
        private volatile bool stopped = false;
        private readonly object lockObject = new object();

        public Server(string ip, int port)
        {
            IP = ip;
            Port = port;

            var endPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(endPoint);
        }

        public override void Start()
        {
            listenThread = new Thread(Listen);
            listenThread.Start();
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

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            helper.Shutdown(SocketShutdown.Both);
            helper.Close();

            adresses.Clear();
        }

        protected override void Listen()
        {
            var buffer = new byte[Constants.MessageBufferSize];
            int size;
            string message;
            char[] delimiterChars = { ';' };
            while (true)
            {
                lock(lockObject)
                {
                    if (stopped)
                    {
                        return;
                    }
                }

                EndPoint endPoint = new IPEndPoint(0, 0);
                size = socket.ReceiveFrom(buffer, ref endPoint);
                message = Encoding.UTF8.GetString(buffer, 0, size);
                
                string[] words = message.Split(delimiterChars, 2);
                if (words[0] == "JOIN")
                {
                    adresses.Add(words[1]);
                    Console.WriteLine($"JOIN: {words[1]}");
                }
                else if (words[0] == "LEAVE")
                {
                    adresses.Remove(words[1]);
                    Console.WriteLine($"LEAVE: {words[1]}");
                }
                else if (words[0] == "ISFREE")
                {
                    socket.SendTo(Encoding.UTF8.GetBytes((!adresses.Contains(words[1])).ToString()), endPoint);
                }
                else
                {
                    // Ignore message
                }
            }
        }
    }
}
