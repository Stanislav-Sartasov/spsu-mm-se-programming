using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using P2PChat.Core;

namespace P2PChat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Client _client;
    public MainWindow()
    {
        InitializeComponent();
        _client = new(new Random().Next(1024, 49151));

        Members.ItemsSource = _client.ConnectedPeers;
        Messages.ItemsSource = _client.Messages;
        YourAddress.Content = "User " + _client.EndPoint.Port;
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
            JoinTextBox.Visibility = Visibility.Collapsed;
            JoinButton.Visibility = Visibility.Collapsed;
            LeaveButton.Visibility = Visibility.Visible;

            var portToConnect = int.Parse(JoinTextBox.Text.Replace(" ", ""));
            if (portToConnect == _client.EndPoint.Port) return;
            foreach (var peer in _client.ConnectedPeers)
            {
                if (portToConnect == peer.Port) return;
            }

            _client.Connect(portToConnect);
        }
    }

    private void LeaveChatClick(object sender, RoutedEventArgs e)
    {
        JoinTextBox.Visibility = Visibility.Visible;
        JoinButton.Visibility = Visibility.Visible;
        LeaveButton.Visibility = Visibility.Collapsed;

        _client.Disconnect();
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