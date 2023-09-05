using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using P2PChat;


namespace P2PChatGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private Peer peer = null;
        private int minPort = 1024;
        private int maxPort = 16384;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Initialize(object sender, RoutedEventArgs e)
        {
            string mPort = myPort.Text;
            string mIp = myIP.Text;

            if (!int.TryParse(mPort, out var port) && minPort <= port && port <= maxPort)
            {
                MessageBox.Show($"IP must be integer between {minPort} and {maxPort}");
                return;
            }


            var success = IPEndPoint.TryParse($"{mIp}:{port}", out var endpoint);
            if (!success)
            {
                MessageBox.Show($"Address {mIp}:{port} is not valid");
                return;
            }
            peer = new Peer(mIp, port);
            peer.Activate();
            peer.OnWpf += Receive;
            Receive($"P2PChat is working on {mIp}:{port}");
            Auth.IsEnabled = false;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (peer == null)
            {
                MessageBox.Show("At first you must be authorized!");
                return;
            }
            peer.Send(messageText.Text);
            messageText.Text = string.Empty;
        }


        private void Receive(string msg)
        {
            this.Dispatcher.Invoke(() => { chatBox.Text += $"{msg}\n"; });
        }

        private void Connect(object sender, RoutedEventArgs e)
        {
            if (peer == null)
            {
                MessageBox.Show("At first you must be authorized!");
                return;
            }
            string servPort = serverPort.Text;
            string servIp = serverIP.Text;

            var success = IPEndPoint.TryParse($"{servIp}:{servPort}", out var endpoint);

            if (success)
            {
                peer.Connect(endpoint);
                Receive($"You are connected to {servIp}:{servPort}");
                serverPort.Text = string.Empty;
                serverIP.Text = string.Empty;
            }
            else
                MessageBox.Show($"Address {servIp}:{servPort} is not valid");
        }

        private void Close(object sender, CancelEventArgs e)
        {
            peer.Dispose();
        }
    }
}
