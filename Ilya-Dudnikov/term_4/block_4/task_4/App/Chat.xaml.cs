using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using P2PChat;
using P2PChat.Message;

namespace P2P_Chat;

public partial class Chat : Window
{
    private readonly Client client;
    private const string WarningMessage = "Your message must be non-empty";

    public Chat(IPAddress userIP, int userPort, IPAddress peerIP, int peerPort)
    {
        client = new Client(new IPEndPoint(userIP, userPort), OnReceivedMessage);
        Console.WriteLine("Connecting to peer");
        client.Join(new IPEndPoint(peerIP, peerPort));
        Console.WriteLine("Connected");
        InitializeComponent();
    }

    private void OnWindowClosing(object sender, CancelEventArgs e)
    {
        client.Dispose();
    }

    private void OnReceivedMessage(string sender, string message)
    {
        sender = sender == client.clientEndpoint.ToString() ? "You" : sender;
        ChatTextBox.Dispatcher.Invoke(new Action(() =>
            ChatTextBox.Text += $"{sender}> {message + Environment.NewLine}"));
    }

    private void SendMessageButton(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Text == "")
        {
            Warning.Text = WarningMessage;
            return;
        }

        Warning.Text = "";
        client.SendMessage(MessageBox.Text);
        MessageBox.Text = "";
    }
}