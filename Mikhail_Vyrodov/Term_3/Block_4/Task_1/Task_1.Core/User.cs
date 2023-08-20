using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Task_1.Core
{
    public class User
    {
        public IPEndPoint UserEndpoint { get; private set; }
        public string Name { get; private set; }

        public delegate void EventHandler(ChatAction chatEvent, string message);

        private event EventHandler onEvent;
        private Socket _socket;
        private object _locker;
        private Thread _runningThread;
        private List<IPEndPoint> _users;
        private volatile bool _isTerminated;

        public User(IPEndPoint endPoint, EventHandler onEvent, string name)
        {
            _users = new List<IPEndPoint>();
            _locker = new();
            this.onEvent += onEvent;
            Name = name;

            UserEndpoint = endPoint;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(UserEndpoint);
            _socket.ReceiveTimeout = 1000;

            _runningThread = new Thread(Run);
            _runningThread.Start();
        }

        public void Disconnect()
        {
            string query = "Disconnect" + ";" + UserEndpoint.ToString() + ";" + Name;
            var serializedQuery = Encoding.UTF8.GetBytes(query);

            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedQuery, _users[i]);
            }
        }

        private void ConnectUsers(string[] msgList)
        {

            for (int i = 1; i < msgList.Length; i++)
            {
                var address = msgList[i].Split(':');
                var endPoint = new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
                if (!_users.Contains(endPoint))
                    _users.Add(endPoint);
            }
        }

        private void ProcessNewConnection(IPEndPoint newUserEndPoint, string newUserName)
        {
            string query = "ConnectUsers" + ";" + String.Join(";", _users.Append(UserEndpoint));
            var serializedConnectQuery = Encoding.UTF8.GetBytes(query);

            _socket.SendTo(serializedConnectQuery, newUserEndPoint);

            string addQuery = "AddUser" + ";" + newUserEndPoint.ToString() + ";" + newUserName; 
            var serializedAddQuery = Encoding.UTF8.GetBytes(addQuery);

            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedAddQuery, _users[i]);
            }
            _users.Add(newUserEndPoint);

            string msg = String.Format("User {0} is connected to User {1}",
                newUserName, Name);
            string successfulConnectMessage = "Message" + ";" + Name + ";" + msg;
            var serializedMsg = Encoding.UTF8.GetBytes(successfulConnectMessage);
            _socket.SendTo(serializedMsg, newUserEndPoint);
        }

        public void Connect(IPEndPoint newUserEndPoint)
        {
            if (newUserEndPoint.ToString() == UserEndpoint.ToString())
            {
                string errorMessage = "You cannot connect to yourself";
                throw new Exception(errorMessage);
            }
            if (_users.Contains(newUserEndPoint))
            {
                string errorMessage = "You are already connected to user with such address";
                throw new Exception(errorMessage);
            }
            
            string query = "Connect" + ";" + UserEndpoint.ToString() + ";" + Name;
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            _socket.SendTo(serializedQuery, newUserEndPoint);
        }

        public void SendMessage(string msg)
        {
            string message = "Message" + ";" + Name + ";" + msg;
            var serializedMessage = Encoding.UTF8.GetBytes(message);
            if (_users.Count == 0)
            {
                string errorMessage = String.Format("User {0} cannot send message because" +
                                                 " it isn't connected to anyone", Name);
                //OnEvent(ChatAction.Error, errorMessage);
                throw new Exception(errorMessage);
            }
            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedMessage, _users[i]);
            }

            string selfMsg = String.Format("{0}: {1}", Name, msg);
            onEvent(ChatAction.Message, selfMsg);
        }

        private string[] GetMessage(ref byte[] buffer)
        {
            lock (_locker)
            {
                if (_isTerminated)
                    throw new Exception("Chat is stopped.");
            }

            int byteCount;
            try
            {
                byteCount = _socket.Receive(buffer);
            }
            catch (Exception _)
            {
                throw new Exception("Cannot get message yet.");
            }

            string msgStr = Encoding.UTF8.GetString(buffer, 0, byteCount);
            var msgList = msgStr.Split(";");

            return msgList;
        }

        private void ProcessMessage(string[] msgList)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, 12345);
            if (msgList[0] == "Connect" || msgList[0] == "Disconnect" || msgList[0] == "AddUser")
            {
                var address = msgList[1].Split(':');
                endPoint = new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
            }

            if (msgList[0] == "Message")
            {
                string message = String.Format("{0}: {1}", msgList[1], msgList[2]);
                onEvent(ChatAction.Message, message);
            }
            else if (msgList[0] == "Connect")
            {
                ProcessNewConnection(endPoint, msgList[2]);
                string message = string.Format("User {0} is connected to User {1}", msgList[2], Name);
                onEvent.Invoke(ChatAction.Connect, message);
            }
            else if (msgList[0] == "Disconnect")
            {
                _users.Remove(endPoint);
                string message = String.Format("User {0} disconnected from User {1}", msgList[2], Name);
                onEvent(ChatAction.Leave, message);

                string msg = String.Format("User {0} disconnected from User {1}",
                    msgList[2], Name);
                string successfulConnectMessage = "Message" + ";" + Name + ";" + msg;
                var serializedMsg = Encoding.UTF8.GetBytes(successfulConnectMessage);
                _socket.SendTo(serializedMsg, endPoint);
            }
            else if (msgList[0] == "ConnectUsers")
            {
                ConnectUsers(msgList);
            }
            else if (msgList[0] == "AddUser")
            {
                _users.Add(endPoint);

                string message = String.Format("User {0} is connected to User {1}", msgList[2], Name);
                onEvent(ChatAction.Message, message);

                string msg = String.Format("User {0} is connected to User {1}",
                    msgList[2], Name);
                string successfulConnectMessage = "Message" + ";" + Name + ";" + msg;
                var serializedMsg = Encoding.UTF8.GetBytes(successfulConnectMessage);
                _socket.SendTo(serializedMsg, endPoint);
            }
        }

        private void Run()
        {
            byte[] buffer = new byte[512];
            string[] msgList = { "" };

            while (true)
            {
                if (_isTerminated)
                    return;
                try
                {
                    msgList = GetMessage(ref buffer);
                }
                catch (Exception e)
                {
                    if (e.Message == "Cannot get message yet.")
                        continue;

                    if (e.Message == "Chat is stopped.")
                        return;
                }

                ProcessMessage(msgList);
            }
        }

        public void Dispose()
        {
            lock (_locker)
            {
                _isTerminated = true;
            }

            _runningThread.Join();

            Disconnect();

            _socket.Close();
            _users.Clear();
        }
    }
}
