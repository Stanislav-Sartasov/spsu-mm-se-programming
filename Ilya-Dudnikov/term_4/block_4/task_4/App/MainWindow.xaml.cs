using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
using P2PChat;

namespace P2P_Chat;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const int MinPort = 1024;
    private const int MaxPort = 49151;

    private const string ErrorHelpMessage =
        "Invalid address.\r\nAvailable IPs: 127.0.0.1-127.255.255.254,\r\navailable ports: 1024-49151";
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RemoveText(object sender, EventArgs e)
    {
        TextBox instance = (TextBox)sender;
        if (instance.Text == instance.Tag.ToString())
        {
            instance.Text = "";
        }
    }

    private void AddText(object sender, EventArgs e)
    {
        TextBox instance = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(instance.Text))
        {
            instance.Text = instance.Tag.ToString();
        }
    }

    private bool ValidateIP(string ip, out IPAddress parsedIP)
    {
        return IPAddress.TryParse(ip, out parsedIP);
    }

    private bool ValidatePort(string port, out int parsedPort)
    {

        return Int32.TryParse(port, out parsedPort) && MinPort <= parsedPort && parsedPort <= MaxPort;
    }

    private bool ValidateInput(string input, out IPAddress parsedIP, out int parsedPort)
    {
        var address = input.Split(':');
        if (address.Length >= 2 && ValidateIP(address[0], out parsedIP) &&
            ValidatePort(address[1], out parsedPort)) return true;
        
        parsedIP = IPAddress.None;
        parsedPort = 0;
        return false;
    }
    
    private void Connect(object sender, RoutedEventArgs e)
    {
        IPAddress userIP, peerIP;
        int userPort, peerPort;
        if (!ValidateInput(Address.Text, out userIP, out userPort) || !ValidateInput(PeerAddress.Text, out peerIP, out peerPort))
        {
            ErrorMessage.Text = ErrorHelpMessage;
            return;
        }


        ErrorMessage.Text = "";
        var chat = new Chat(userIP, userPort, peerIP, peerPort);
        chat.Show();
    }
}