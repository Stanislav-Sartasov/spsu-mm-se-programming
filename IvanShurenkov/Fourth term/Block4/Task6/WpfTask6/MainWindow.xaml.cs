using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
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
using System.Windows.Threading;
using Task6.Network;

namespace WpfTask6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client user = null;
        UIReceiver receiver = null;
        private int buttonNumber = 0;

        public delegate void WaitTextUpdateDelegate();
        public MainWindow()
        {
            InitializeComponent();
            Start.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new WaitTextUpdateDelegate(WhileCheck));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (user != null)
            {
                Thread stop = new Thread(() => user.Stop());
                stop.Start();
                Chat.Text = "";
                stop.Join();
            }
        }

        private void WhileCheck()
        {
            if (receiver != null && receiver.Text != String.Empty)
            {
                UpdateTextData();
            }
            Thread.Sleep(10);
            switch (buttonNumber % 3)
            {
                case 0:
                    Start.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new WaitTextUpdateDelegate(WhileCheck));
                    break;
                case 1:
                    Send.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new WaitTextUpdateDelegate(WhileCheck));
                    break;
                case 2:
                    Connect.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new WaitTextUpdateDelegate(WhileCheck));
                    break;
            }
            buttonNumber = (buttonNumber + 1) % 3;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                int port;
                try
                {
                    port = int.Parse(Port.Text);
                }
                catch (FormatException ex)
                {
                    //Console.WriteLine(ex.Message);
                    MessageBox.Show("Invalid Port");
                    return;
                }
                catch (ArgumentNullException)
                {
                    //Console.WriteLine(ex.Message);
                    MessageBox.Show("Invalid Port");
                    return;
                }
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(IP.Text);
                }
                catch (FormatException ex)
                {
                    //Console.WriteLine(ex.Message);
                    MessageBox.Show("Invalid IP Address");
                    return;
                }
                catch (ArgumentNullException)
                {
                    //Console.WriteLine(ex.Message);
                    MessageBox.Show("Invalid IP Address");
                    return;
                }
                try
                {
                    user.Connect(ip, port);
                    UpdateTextData();
                    Start.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new WaitTextUpdateDelegate(WhileCheck));
                }
                catch (FormatException ex)
                {
                    //Console.WriteLine(ex.Message);
                    MessageBox.Show("Invalid IP Address or Port");
                }
            }
            else
            {
                MessageBox.Show("Need to start");
            }
        }

        private void UpdateTextData()
        {
            MyConnectionData.Text = String.Format("{0}:{1}", user.IPAddr, user.Port);
            lock (receiver.Text)
            {
                Chat.Text += receiver.Text;
                receiver.Text = String.Empty;
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                user.Stop();
            }
            int port = 5000;
            try
            {
                port = int.Parse(MyPort.Text);

                receiver = new UIReceiver();
                user = new Client(port, receiver);
                UpdateTextData();
            }
            catch (FormatException ex)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("Invalid Port");
                return;
            }
            catch (ArgumentNullException)
            {
                //Console.WriteLine(ex.Message);
                MessageBox.Show("Invalid Port");
                return;
            }
            Start.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new WaitTextUpdateDelegate(WhileCheck));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                user.Send(Message.Text);
                UpdateTextData();
            }
            else
            {
                MessageBox.Show("Need to start");
            }
            Start.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new WaitTextUpdateDelegate(WhileCheck));
        }

        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
