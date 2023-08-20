using System;
using System.Net;
using System.Windows;
using Task_1.Core;
using System.ComponentModel;

namespace Task_1.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User? _user;
        private bool _isConnected = false;

        public MainWindow()
        {
            InitializeComponent();

            string firstMsg = "To create new user you must enter your user's address and name.\n" +
                              " After that you must click \"Start Server\" button";

            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(firstMsg);
            });

            Closing += OnWindowClosing;
        }

        private void EventHandler(ChatAction chatEvent, string message)
        {
            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(message);
            });

            if (chatEvent == ChatAction.Connect)
            {
                _isConnected = true;
            }
        }

        private bool ProcessNewConnection(string[] msgList)
        {
            var address = msgList[1].Split(':');
            IPEndPoint endPoint;
            try
            {
                endPoint = new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
            }
            catch (Exception _)
            {
                MessageBox.Show("Invalid address of user you want to connect to.");
                return false;
            }

            try
            {
                _user.Connect(endPoint);
                _isConnected = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
            return true;
        }

        private bool ProcessNewMessage(string message)
        {
            if (_isConnected == false)
            {
                MessageBox.Show("You need to connect to some chat or create" +
                                " your own before sending messages.");
                return false;
            }

            try
            {
                _user.SendMessage(message);
            }
            catch (Exception exception)
            {
                var errorMessage = String.Format("User {0} cannot send message because" +
                                                 " it isn't connected to anyone", _user.Name);
                if (exception.Message == errorMessage)
                {
                    MessageBox.Show(errorMessage);
                }
                return false;
            }

            return true;
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;

            if (message == "")
            {
                MessageBox.Show("Type something before sending message.");
                return;
            }

            if (_user is null)
            {
                MessageBox.Show("You need to create your user before sending messages.");
                return;
            }

            var msgList = message.Split(' ');
            bool clearFlag;
            if (msgList.Length == 2 && msgList[0] == "Connect")
            {
                clearFlag = ProcessNewConnection(msgList);
            }
            else
            {
                clearFlag = ProcessNewMessage(message);
            }
            if (clearFlag) 
                messageTextBox.Clear();
        }

        private void DisconnectClick(object sender, RoutedEventArgs e)
        {
            string message = messageTextBox.Text;

            if (_user is null || _isConnected == false)
            {
                MessageBox.Show("You need to create your user and start chatting with" +
                                " someone before disconnecting from existing chat.");
                return;
            }

            _user.Disconnect();
            _isConnected = false;

            messageTextBox.Clear();
        }

        private void StartServerButtonClick(object sender, RoutedEventArgs e)
        {
            var strAddress = portTextBox.Text;
            var name = nameTextBox.Text;

            if (name == "" || strAddress == "")
            {
                MessageBox.Show("Enter user name and address before starting chat.");
                return;
            }

            var address = strAddress.Split(':');

            if (address.Length != 2)
            {
                MessageBox.Show("Invalid address.");
                return;
            }

            IPEndPoint endPoint;
            try
            {
                endPoint = new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
            }
            catch (Exception _)
            {
                MessageBox.Show("Invalid ip or port.");
                return;
            }

            _user = new User(endPoint, EventHandler, name);

            startServerButton.IsEnabled = false;
            portTextBox.IsEnabled = false;
            nameTextBox.IsEnabled = false;

            string initialMessage = "Hi. You just created new user. To start chatting you need to enter address of User" +
                                  " that you want to create new chat with\nor wait for other user to connect to you.\n" +
                                  "To connect to other user you should enter message like this - \"Connect {user's address}\"\n" +
                                  "If connection is successful, than connected user would receive a message about new connection\n" +
                                  "Also you can disconnect from chat, if you're already in some chat.\n" +
                                  "For that you can press the \"Disconnect\" button";
            Dispatcher.Invoke(() =>
            {
                chatListBox.Items.Add(initialMessage);
            });
        }

        private void OnWindowClosing(object? sender, CancelEventArgs e)
        {
            if (_user is not null)
                _user.Dispose();
        }
    }
}
