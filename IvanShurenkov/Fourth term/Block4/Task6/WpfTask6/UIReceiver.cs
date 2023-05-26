using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task6.Network;
using static System.Net.Mime.MediaTypeNames;

namespace WpfTask6
{
    internal class UIReceiver : IReceiver
    {
        public string Text = String.Empty;

        void IReceiver.Connect(EndPoint endPoint, string status)
        {
            lock (Text)
            {
                if (status == "Y\n")
                    Text += String.Format("Connect to {0}\n", endPoint);
                else
                    Text += String.Format("Not connected\n");
            }
        }

        void IReceiver.Receive(IPAddress remoteIP, string text)
        {
            lock (Text)
            {
                Text += String.Format("{0}: {1}", remoteIP, text);
            }
        }

        void IReceiver.Send(string text)
        {
            lock (Text)
            {
                Text += String.Format("Me: {0}\n", text);
            }
        }
    }
}
