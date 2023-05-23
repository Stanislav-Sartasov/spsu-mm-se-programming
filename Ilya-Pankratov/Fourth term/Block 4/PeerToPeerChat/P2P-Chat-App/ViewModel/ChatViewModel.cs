using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.ViewModel
{
    internal class ChatViewModel : AViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<PeerModel> Peers { get; set; }
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SendCommand {get; set;}
        public RelayCommand DisconnectCommand { get; set; }
        private readonly INavigationService nagivateService;

        public ChatViewModel(INavigationService nagivateService)
        {
            Messages = new ObservableCollection<MessageModel>();
            Peers = new ObservableCollection<PeerModel>();
            this.nagivateService = nagivateService;

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel()
                {
                    Content = Message,
                    FirstMessage = false,
                });

                Message = string.Empty;
            }, canExecute => true);

            // Test

            Messages.Add(new MessageModel()
            {
                Username = "Pankrat",
                Time = DateTime.Now,
                Content = "Test",
                IsNativeOrigin = true,
                FirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel()
                {
                    Username = "Pankrat",
                    Time = DateTime.Now,
                    Content = "Test" + i,
                    IsNativeOrigin = true,
                    FirstMessage = false
                });
            }

            Messages.Add(new MessageModel()
            {
                Username = "Ilya",
                Time = DateTime.Now,
                Content = "Test_Ilya",
                IsNativeOrigin = false,
                FirstMessage = true,
            });

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel()
                {
                    Username = "Ilya",
                    Time = DateTime.Now,
                    Content = "Test" + i,
                    IsNativeOrigin = false,
                    FirstMessage = false
                });
            }

            for (int i = 0; i < 4; i++)
            {
                Messages.Add(new MessageModel()
                {
                    Username = "Ilya",
                    Time = DateTime.Now,
                    Content = "Test" + i,
                    IsNativeOrigin = true,
                    FirstMessage = false
                });
            }

            Messages.Add(new MessageModel()
            {
                Username = "Ilya",
                Time = DateTime.Now,
                Content = "Last",
                IsNativeOrigin = true,
                FirstMessage = false
            });

            for (int i = 0; i < 5; i++)
            {
                Peers.Add(new PeerModel
                {
                    Username = "Ilysha" + i,
                    Messages = Messages
                }) ;
            }
        }

    }
}
