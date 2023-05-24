using Core;
using Core.Chat;
using Core.Data;
using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using System.Net;
using System.Threading;

namespace P2P_Chat_App.ViewModel
{
    internal class ConnectViewModel : AViewModel
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
        private readonly int timeout;

        public string LocalIp
        {
            get
            {
                return "Local IP: " + localIp.ToString();
            }
        }

        public string Port { get; set; }
        public string IPEndPoint { get; set; }

        public RelayCommand ConnectCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public IClient<Message, Peer> Client { get; }

        public ConnectViewModel(INavigationService nagivateService, CurrentUserModel user, IClient<Message, Peer> client)
        {
            Port = string.Empty;
            IPEndPoint = string.Empty;
            this.nagivateService = nagivateService;
            this.user = user;
            Client = client;
            Client.OnConnectionSuccessed = OnConnectionSuccessed;
            Client.OnConnectionFailed = OnConnectionFailed;
            timeout = 10000;
            localIp = user.LocalIpAddress;

            BackCommand = new RelayCommand(o =>
            {
                NagivateService.NavigateTo<MainViewModel>();
            }, canExecute => true);

            ConnectCommand = new RelayCommand(o =>
            {
                // Validate data

                var data = IPEndPoint.Split(":");

                if (data.Length != 2)
                {
                    return;
                }

                if (!int.TryParse(Port.Trim(), out var localPort))
                {
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
                user.LocalPort = localPort;
                user.RemotePort = remotePort;
                user.RemoteIpAddress = remoteIPAddress;

                Client.Start(user.LocalPort);
                var thread = new Thread(async () => await Client.ConnectToClient(user.RemotePort, user.RemoteIpAddress, timeout));
                thread.Start();

                NagivateService.NavigateTo<LoadingViewModel>();
            }, canExecute => true);
        }

        private void OnConnectionSuccessed()
        {
            user.IsConnected = true;
            nagivateService.NavigateTo<ChatViewModel>();
        }

        private void OnConnectionFailed()
        {
            user.IsConnected = false;
            Client.Stop();
            nagivateService.NavigateTo<MainViewModel>();
        }
    }
}
