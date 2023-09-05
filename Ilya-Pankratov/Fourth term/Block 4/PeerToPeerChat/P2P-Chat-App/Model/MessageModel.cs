using System;

namespace P2P_Chat_App.Model
{
    internal class MessageModel
    {
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public bool IsNativeOrigin { get; set; }
        public bool? FirstMessage { get; set; }

    }
}
