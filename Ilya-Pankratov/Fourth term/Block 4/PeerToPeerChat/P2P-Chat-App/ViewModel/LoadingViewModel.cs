using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using P2P_Chat_App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2P_Chat_App.ViewModel
{
    internal class LoadingViewModel : AViewModel
    {
        private readonly INavigationService nagivateService;
        private volatile bool isConnected;

        public CurrentUserModel User { get; }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
            private set
            {
                isConnected = value;
            }
        }

        public LoadingViewModel(INavigationService nagivateService, CurrentUserModel user)
        {
            var thread = new Thread(ConnectToRemoteUser);
            thread.Start();
            this.nagivateService = nagivateService;
            this.User = user;
        }

        private void ConnectToRemoteUser()
        {
            Thread.Sleep(3000);
            IsConnected = true;
            nagivateService.NavigateTo<ChatViewModel>();
        }
    }
}
