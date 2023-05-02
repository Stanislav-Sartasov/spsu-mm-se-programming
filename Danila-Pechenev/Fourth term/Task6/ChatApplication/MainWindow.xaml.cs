using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using ChatParticipants;

namespace ChatApplication
{
    public partial class MainWindow : Window, IDisposable
    {
        private ChatWindow chatWindow;

        private string userName;
        private string userIP;
        private int userPort;

        private Client client;
        private bool clientCreated = false;

        public MainWindow()
        {
            Left = 150;
            Top = 50;
            InitializeComponent();
        }

        private bool ValidateUserInfo()
        {
            if (clientCreated)
            {
                return false;
            }

            bool success = Validator.ValidateUserName(UserName.Text, out string errorMessage);
            ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return false;
            }

            userName = UserName.Text;
            success = Validator.ValidateIP(UserIP.Text, out errorMessage, out IPAddress ip);
            ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return false;
            }

            userIP = ip.ToString();
            success = Validator.ValidatePort(UserPort.Text, out errorMessage, out userPort);
            ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return false;
            }

            try
            {
                success = CheckIfAddressIsFree(userIP, userPort);
                if (!success)
                {
                    ErrorMessageField.Text = "This address (IP + port) has alreadly been used. Сhoose another one";
                    return false;
                }
            }
            catch
            {
                ErrorMessageField.Text = "The server is not available";
                return false;
            }

            return true;
        }

        private bool CheckIfAddressIsFree(string userIP, int userPort)
        {
            if (userIP == Constants.ServerIP && userPort == Constants.ServerPort)
            {
                return false;
            }

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(new IPEndPoint(0, 0));

            EndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Constants.ServerIP), Constants.ServerPort);
            socket.SendTo(Encoding.UTF8.GetBytes($"ISFREE;{userIP};{userPort}"), serverEndPoint);

            var buffer = new byte[Constants.MessageBufferSize];
            int size = socket.Receive(buffer);  // If the server is not available, there will be an exception here
            string result = Encoding.UTF8.GetString(buffer, 0, size);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            if (result == "False")
            {
                return false;
            }

            return true;
        }

        private void OpenChat()
        {
            chatWindow = new ChatWindow(this);
            chatWindow.UserInfoField.Text = $"[{userName}, IP: {userIP}, Port: {userPort}] Your chat";
            chatWindow.Left = Left;
            chatWindow.Top = Top;
            chatWindow.Show();
            Close();
        }

        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = ValidateUserInfo();
            if (!success)
            {
                return;
            }

            OpenChat();

            client = new Client(userIP, userPort, chatWindow.PrintMessage);
            clientCreated = true;
            client.Start();
            SendUserJoinedChatMessage();
        }

        private void JoinChatButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = ValidateUserInfo();
            if (!success)
            {
                return;
            }

            success = Validator.ValidateIP(ParticipantIP.Text, out string errorMessage, out IPAddress ip);
            ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return;
            }

            var participantIP = ip.ToString();
            success = Validator.ValidatePort(ParticipantPort.Text, out errorMessage, out int participantPort);
            ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return;
            }

            bool addressIsFree = CheckIfAddressIsFree(participantIP, participantPort);
            if (addressIsFree)
            {
                ErrorMessageField.Text = "There is no registered user with this address (IP + port)";
                return;
            }
            else if (participantIP == Constants.ServerIP && participantPort == Constants.ServerPort)
            {
                ErrorMessageField.Text = "You cannot connect to the server";
                return;
            }

            OpenChat();

            client = new Client(userIP, userPort, chatWindow.PrintMessage);
            clientCreated = true;
            client.Start(participantIP, participantPort);
            SendUserJoinedChatMessage();
        }

        public bool SendMessage(string message)
        {
            bool success = Validator.ValidateMessage(message, out string errorMessage);
            chatWindow.ErrorMessageField.Text = errorMessage;
            if (!success)
            {
                return false;
            }

            string time = DateTime.Now.TimeOfDay.ToString().Split(new char[] { '.' })[0];
            string fullMessage = $"[{time}] {userName}: {message}{Environment.NewLine}";
            client.SendMessage(fullMessage);
            return true;
        }

        private void SendUserJoinedChatMessage()
        {
            string time = DateTime.Now.TimeOfDay.ToString().Split(new char[] { '.' })[0];
            client.SendMessage($"[{time}] User {userName} joined the chat{Environment.NewLine}");
        }

        private void SendUserLeavedChatMessage()
        {
            string time = DateTime.Now.TimeOfDay.ToString().Split(new char[] { '.' })[0];
            client.SendMessage($"[{time}] User {userName} leaved the chat{Environment.NewLine}");
        }

        public void Dispose()
        {
            SendUserLeavedChatMessage();
            if (clientCreated)
            {
                client.Dispose();
            }
        }
    }
}
