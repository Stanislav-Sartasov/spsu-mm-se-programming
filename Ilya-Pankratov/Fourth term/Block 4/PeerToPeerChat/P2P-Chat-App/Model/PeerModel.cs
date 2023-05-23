using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.Model
{
    internal class PeerModel
    {
        public string Username { get; set; }
        public ObservableCollection<MessageModel> Messages { get; set; }
        public string LastMesage => Messages.Last().Content;
    }
}
