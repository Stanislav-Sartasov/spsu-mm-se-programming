using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using P2PChat.Core;

namespace P2PChat.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Client _client;
    public MainWindow()
    {
        InitializeComponent();
        _client = new(10);
        Members.ItemsSource = _client.ConnectedPeers;
        _client.ConnectedPeers.Add(new IPEndPoint(IPAddress.Any, 10));
        _client.ConnectedPeers.Add(new IPEndPoint(IPAddress.Any, 11));
        Messages.ItemsSource = _client.Messages;
        _client.Messages.Add(new Message(MessageType.Text, _client.EndPoint, "hello"));
        YourAddress.Content = _client.EndPoint;
    }

    private void OnNewMessage(Message msg)
    {
        // если отключается или подклбчается то просто убирать или добавлять в списке слева
        // тут 2 списка, один со своими сообщениями, другой с чужими
        // сравниваем эндпоинты и добавляем в 1 из списков соответственно
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
        Application.Current.Shutdown();
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
        }
        //_client.Connect();
    }

    private void LeaveChatClick(object sender, RoutedEventArgs e)
    {
        JoinTextBox.Visibility = Visibility.Visible;
        JoinButton.Visibility = Visibility.Visible;
        LeaveButton.Visibility = Visibility.Collapsed;
        //_client.Disconnect();
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