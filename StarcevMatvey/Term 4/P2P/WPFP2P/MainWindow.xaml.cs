using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using P2P.Chat;
using P2P.Utils;

namespace WPFP2P
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client _client;


        public MainWindow()
        {
            InitializeComponent();

            var port = new Random().Next(0, 1000);
            _client = new Client(port, IReceive, Receive);
            IReceive($"I start working at port {port}");
        }

        private void Receive(string msg, string adress)
        {
            Dispatcher.Invoke(() => { chat.Text += $"{adress} sended {msg}\n"; });
        }

        private void IReceive(string msg)
        {
            Dispatcher.Invoke(() => { chat.Text += $"You sended {msg}\n"; });
        }

        private void ErrorMes(string msg)
        {
            Dispatcher.Invoke(() => { chat.Text += $"{msg}\n"; });
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            _client.Send(messenge.Text);
            messenge.Text = "";
        }

        private void Conect(object sender, RoutedEventArgs e)
        {
            var a = adrres.Text;
            var adr = Utils.TryPort(a);

            if (adr == null) ErrorMes($"I can't connect to {a}");
            else
            {
                _client.Conect(adr);
                clients.Text += $"{adr}\n";
            }

            adrres.Text = "";
        }

        private void Relogin(object sender, RoutedEventArgs e)
        {
            var a = adrres.Text;
            var adr = Utils.TryPort(a);

            if (adr == null) ErrorMes($"I cant't relogin at {a}");
            else
            {
                _client = new Client(adr, IReceive, Receive);
                ErrorMes($"I relogin at {a}");
                clients.Text = "";
            }

            adrres.Text = "";
        }

        private void ClearChat(object sender, RoutedEventArgs e)
        {
            chat.Text = "";
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            _client.Dispose();
        }
    }
}
