using Core;
using Core.Chat;
using Core.Data;
using Core.Network;
using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using P2P_Chat_App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace P2P_Chat_App.ViewModel
{
    internal class LoadingViewModel : AViewModel
    {
        private readonly INavigationService nagivateService;
        private volatile bool isConnected;

        public CurrentUserModel User { get; }
        public IClient<Message, Peer> Client { get; }
        public IPEndPoint RemoteIPEndPoint
        {
            get
            {
                return new IPEndPoint(User.RemoteIpAddress, User.RemotePort);
            }
        }

        public LoadingViewModel(INavigationService nagivateService, CurrentUserModel user, IClient<Message, Peer> clientNode)
        {
            Client = clientNode;
            this.nagivateService = nagivateService;
            User = user;
        }
    }
}
