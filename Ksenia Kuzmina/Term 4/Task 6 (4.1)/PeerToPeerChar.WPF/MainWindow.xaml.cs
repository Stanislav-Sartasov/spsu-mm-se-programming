using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using PeerToPeerChat.Chat;


namespace ChatWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ChatClient chat;
        private int port;
        private string name;

        public MainWindow()
        {
            port = new Random().Next() % (65536 - 10000) + 10000;

            InitializeComponent();
            chat = new ChatClient(port);
            chat.OnMessage += Receive;
            Receive("Chat is working on port " + port);
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            chat.Send(messageText.Text);
            messageText.Text = string.Empty;
        }


        private void Receive(string msg)
        {
            this.Dispatcher.Invoke(() => { chatBox.Text += msg + "\n"; });
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            string servPort = serverPort.Text;
            string servIp = serverIP.Text;

            var success = IPEndPoint.TryParse(servIp + ":" + servPort, out var endpoint);

            if (success)
            {
                chat.ConnectTo(endpoint);
                Receive("Chat is working on port " + servPort);
                serverPort.Text = string.Empty;
                serverIP.Text = string.Empty;
            }
            else
                MessageBox.Show(servIp + ":" + servPort + " не является верным адресом");
        }

        private void ClearChat(object sender, RoutedEventArgs e)
        {
            chatBox.Text = string.Empty;
        }

        private void Close(object sender, CancelEventArgs e)
        {
            chat.Dispose();
        }
    }
}
