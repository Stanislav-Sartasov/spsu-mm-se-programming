using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using P2P_Chat_App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.ViewModel
{
    internal class MainViewModel : AViewModel
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

        public string Username { get; set; }
        public string Port { get; set; }
        public string IPEndPoint { get; set; }

        public RelayCommand ConnectCommand { get; set; }

        public MainViewModel(INavigationService nagivateService, CurrentUserModel user)
        {
            Username = string.Empty;
            Port = string.Empty;
            IPEndPoint = string.Empty;

            ConnectCommand = new RelayCommand(o =>
            {
                // Validate data

                var data = IPEndPoint.Split(":");

                if(data.Length != 2)
                {
                    return;
                }

                if (!int.TryParse(Port.Trim(), out var localPort)) {
                    return;
                }

                if (!int.TryParse(data[1].Trim(), out var remotePort))
                {
                    return;
                }

                if (!IPAddress.TryParse(data[0].Trim(), out var remoteIPAddress))
                {
                    return;
                }

                // Set userData
                user.Name = Username;
                user.LocalPort = localPort;
                user.RemotePort = remotePort;
                user.RemoteIpAddress = remoteIPAddress;

                NagivateService.NavigateTo<LoadingViewModel>();
            }, canExecute => true);

            this.nagivateService = nagivateService;
            this.user = user;
        }
    }
}
