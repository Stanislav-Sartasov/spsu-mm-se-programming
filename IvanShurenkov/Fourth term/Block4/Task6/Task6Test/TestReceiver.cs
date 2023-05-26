using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Task6.Network;

namespace Task6Test
{
    internal class TestReceiver : IReceiver
    {
        public List<string> Log = new List<string>();
        private int _id = 0;

        public TestReceiver(int id)
        {
            _id = id;
        }

        void IReceiver.Connect(EndPoint endPoint, string status)
        {
            Log.Add(endPoint.ToString());
        }

        void IReceiver.Receive(IPAddress remoteIP, string text)
        {
            Log.Add("R" + text);
        }

        void IReceiver.Send(string text)
        {
            Log.Add("S" + text + "\n");
        }
    }
}
