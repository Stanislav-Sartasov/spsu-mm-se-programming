using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Network
{
    public interface IReceiver
    {
        public void Receive(IPAddress remoteIP, string text);
        public void Send(string text);
        public void Connect(EndPoint endPoint, string status);
    }
}
