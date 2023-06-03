using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using P2PChat.Core;

namespace P2PChat.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Client _client;
    private readonly ObservableCollection<IPEndPoint> _connectedPeers = new();
    private readonly ObservableCollection<Message> _messages = new();

    public MainWindow()
    {
        InitializeComponent();

        _client = new(new Random().Next(1024, 49151), OnNewMessage);

        Members.ItemsSource = _connectedPeers;
        Messages.ItemsSource = _messages;
        YourAddress.Content = "User " + _client.EndPoint.Port;
    }

    private void OnNewMessage(Message msg)
    {
        Dispatcher.Invoke(delegate
        {
            switch (msg.Type)
            {
                case MessageType.Text:
                    _messages.Add(msg);
                    break;
                case MessageType.AddPeer:
                    _connectedPeers.Add(IPEndPoint.Parse(msg.Sender));
                    ShowLeaveButton();
                    break;
                case MessageType.RemovePeer:
                    _connectedPeers.Remove(IPEndPoint.Parse(msg.Sender));
                    if (!_connectedPeers.Any())
                        ShowJoinButton();
                    break;
            }
        });
    }

    private void BorderMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }


    private void MinimizeButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow.WindowState = WindowState.Minimized;
    }

    private void WindowStateButtonClick(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState != WindowState.Maximized
            ? WindowState.Maximized
            : WindowState.Normal;
    }


    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        _client.Dispose();
        Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
    }

    private void SendMessageClick(object sender, RoutedEventArgs e)
    {
        _client.SendMessage(MessageBox.Text);
        MessageBox.Clear();
    }

    private void JoinChatClick(object sender, RoutedEventArgs e)
    {
        if (JoinTextBox.Text != "")
        {
            var portToConnect = int.Parse(JoinTextBox.Text.Replace(" ", ""));
            if (portToConnect == _client.EndPoint.Port) return;
            foreach (var peer in _connectedPeers)
            {
                if (portToConnect == peer.Port) return;
            }

            _client.Connect(portToConnect);
        }
    }
    private void ShowLeaveButton()
    {
        PortToJoinTextBox.Visibility = Visibility.Collapsed;
        JoinButton.Visibility = Visibility.Collapsed;
        LeaveButton.Visibility = Visibility.Visible;
    }

    private void LeaveChatClick(object sender, RoutedEventArgs e)
    {
        ShowJoinButton();
        _client.Disconnect();
    }

    private void ShowJoinButton()
    {
        PortToJoinTextBox.Visibility = Visibility.Visible;
        JoinButton.Visibility = Visibility.Visible;
        LeaveButton.Visibility = Visibility.Collapsed;
    }

    private void PortTextBoxPreviewInput(object sender, TextCompositionEventArgs e)
    {
        if (!IsTextNumeric(e.Text))
        {
            e.Handled = true;
        }
    }

    private void PortTextBoxPasting(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            string text = (string)e.DataObject.GetData(typeof(string));
            if (!IsTextNumeric(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    private bool IsTextNumeric(string text)
    {
        Regex regex = new Regex("[^0-9]+");
        return !regex.IsMatch(text);
    }
}