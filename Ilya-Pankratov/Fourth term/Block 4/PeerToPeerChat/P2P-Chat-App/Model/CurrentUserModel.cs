using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.Model
{
    internal class CurrentUserModel
    {
        public string Name { get; set; }
        public int LocalPort { get; set; }
        public IPAddress RemoteIpAddress { get; set; }
        public int RemotePort { get; set; }
    }
}
