using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Task_1.Core
{
    public class User
    {
        public IPEndPoint UserEndPoint { get; private set; }
        public string Name { get; private set; }

        public delegate void EventHandler(ChatAction chatEvent, string message);

        private event EventHandler onEvent;
        private Socket _socket;
        private object _locker;
        private Thread _runningThread;
        private List<Tuple<IPEndPoint, string>> _users;
        private volatile bool _isTerminated;

        public User(IPEndPoint endPoint, EventHandler onEvent, string name)
        {
            _users = new List<Tuple<IPEndPoint, string>>();
            _locker = new();
            this.onEvent += onEvent;
            Name = name;

            UserEndPoint = endPoint;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(UserEndPoint);
            _socket.ReceiveTimeout = 1000;

            _runningThread = new Thread(Run);
            _runningThread.Start();
        }

        public void Disconnect()
        {
            var serializedQuery = MessageConverter.CreateDisconnectQuery(UserEndPoint, Name);

            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedQuery, _users[i].Item1);
            }
        }

        private void ConnectUsers(string[] msgList)
        {
            for (int i = 1; i < msgList.Length; i++)
            {
                var userTuple = MessageConverter.ParseUserTuple(msgList[i]);
                if (!_users.Contains(userTuple))
                    _users.Add(userTuple);
            }
        }

        private void ProcessNewConnection(IPEndPoint newUserEndPoint, string newUserName)
        {
            var tupleInfo = new Tuple<IPEndPoint, string>(UserEndPoint, Name);
            var serializedConnectQuery = 
                MessageConverter.CreateConnectUsersQuery(_users.Append(tupleInfo));

            _socket.SendTo(serializedConnectQuery, newUserEndPoint);

            var serializedAddQuery = MessageConverter.CreateAddUserQuery(newUserEndPoint, newUserName);

            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedAddQuery, _users[i].Item1);
            }
            if (!_users.Any(x => x.Item1.Equals(newUserEndPoint)))
                _users.Add(new Tuple<IPEndPoint, string>(newUserEndPoint, newUserName));

            var serializedNotification = 
                MessageConverter.CreateConnectionNotification(Name, newUserName);
            _socket.SendTo(serializedNotification, newUserEndPoint);
        }

        public void Connect(IPEndPoint newUserEndPoint)
        {
            InnerConnect(newUserEndPoint, UserEndPoint, Name);

            for (int i = 0; i < _users.Count; i++)
            {
                InnerConnect(newUserEndPoint, _users[i].Item1, _users[i].Item2);
            }
        }

        private void InnerConnect(IPEndPoint newUserEndPoint, IPEndPoint userEndPoint, string newUserName)
        {
            if (newUserEndPoint.ToString() == UserEndPoint.ToString())
            {
                string errorMessage = "You cannot connect to yourself";
                throw new Exception(errorMessage);
            }
            if (_users.Any(x => x.Item1.Equals(newUserEndPoint)))
            {
                string errorMessage = "You are already connected to user with such address";
                throw new Exception(errorMessage);
            }

            var serializedQuery = MessageConverter.CreateConnectQuery(userEndPoint, newUserName);
            _socket.SendTo(serializedQuery, newUserEndPoint);
        }

        public void SendMessage(string msg)
        {
            var serializedMessage = MessageConverter.CreateMessageQuery(Name, msg);
            if (_users.Count == 0)
            {
                string errorMessage = String.Format("User {0} cannot send message because" +
                                                 " it isn't connected to anyone", Name);
                throw new Exception(errorMessage);
            }
            for (int i = 0; i < _users.Count; i++)
            {
                _socket.SendTo(serializedMessage, _users[i].Item1);
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
            var endPoint = new IPEndPoint(IPAddress.Any, 1234);
            if (msgList[0] == "Connect" || msgList[0] == "Disconnect" || msgList[0] == "AddUser")
            {
                endPoint = MessageConverter.ParseEndPoint(msgList[1]);
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
                var tupleToRemove = new Tuple<IPEndPoint, string>(endPoint, msgList[2]);
                _users.Remove(tupleToRemove);

                string message = String.Format("User {0} disconnected from User {1}", msgList[2], Name);
                onEvent(ChatAction.Leave, message);


                var serializedDisconnectMessage = 
                    MessageConverter.CreateDisconnectionNotification(Name, msgList[2]);
                _socket.SendTo(serializedDisconnectMessage, endPoint);
            }
            else if (msgList[0] == "ConnectUsers")
            {
                ConnectUsers(msgList);
            }
            else if (msgList[0] == "AddUser")
            {
                var newUserTuple = new Tuple<IPEndPoint, string>(endPoint, msgList[2]);
                if (_users.Contains(newUserTuple))
                {
                    return;
                }

                _users.Add(newUserTuple);
                string message = String.Format("User {0} is connected to User {1}", msgList[2], Name);
                onEvent(ChatAction.Message, message);

                var serializedConnectMessage =
                    MessageConverter.CreateConnectionNotification(Name, msgList[2]);
                _socket.SendTo(serializedConnectMessage, endPoint);
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
