using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task6.Network;

namespace Task6
{
    internal class SimpleReceiver: IReceiver
    {
        public void Receive(IPAddress remoteIP, string text)
        {
            Console.Write("{0}| {1}", remoteIP, text);
        }
        public void Send(string text)
        {
            Console.WriteLine("Send: {0}", text);
        }

        public void Connect(EndPoint endPoint, string status)
        {
            Console.WriteLine("Connected to {0}|{1}", endPoint, status);
        }
    }
}
