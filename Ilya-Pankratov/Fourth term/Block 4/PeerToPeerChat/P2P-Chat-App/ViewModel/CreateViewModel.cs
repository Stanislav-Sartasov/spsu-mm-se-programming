using Core.Chat;
using Core.Data;
using Core;
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
    internal class CreateViewModel : AViewModel
    {
        private INavigationService nagivateService;

        private CurrentUserModel user;
        public INavigationService NagivateService
        {
            get
            {
                return nagivateService;
            }
            set
            {
                nagivateService = value;
                OnPropertyChanged();
            }
        }

        private IPAddress localIp;
        public string LocalIp { get { return "Local IP: " + localIp.ToString(); } }
        public string Port { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public CreateViewModel(INavigationService nagivateService, CurrentUserModel user, IClient<Message, Peer> clientNode)
        {
            Port = string.Empty;
            localIp = user.LocalIpAddress;
            this.nagivateService = nagivateService;
            this.user = user;

            BackCommand = new RelayCommand(o =>
            {
                NagivateService.NavigateTo<MainViewModel>();
            }, canExecute => true);

            CreateCommand = new RelayCommand(o =>
            {
                // Validate data
                if (!int.TryParse(Port.Trim(), out var localPort))
                {
                    return;
                }

                if (!NetworkManager.IsPortValid(localPort))
                {
                    return;
                }

                // Set userData
                user.LocalPort = localPort;
                clientNode.Start(user.LocalPort);
                NagivateService.NavigateTo<ChatViewModel>();
            }, canExecute => true);
        }
    }
}
