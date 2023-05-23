using Core;
using Core.Chat;
using Core.Data;
using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2P_Chat_App.ViewModel
{
    internal class ChatViewModel : AViewModel
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<PeerModel> Peers { get; set; }
        public string IpEndPoint 
        { 
            get
            {
                return $"{User.LocalIpAddress}:{User.LocalPort}";
            }
        }

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
        public INavigationService NavigateService { get; }
        public CurrentUserModel User { get; }
        public IClient<Message, Peer> Client { get; }
        public ChatViewModel(INavigationService nagivateService, CurrentUserModel user, IClient<Message, Peer> clientNode)
        {
            Messages = new ObservableCollection<MessageModel>();
            Peers = new ObservableCollection<PeerModel>();
            NavigateService = nagivateService;
            User = user;
            Client = clientNode;
            Client.OnNewConnection = ThreadSaveAddPeer;
            Client.OnDisconnection = ThreadSaveRemovePeer;
            Client.OnMessageReceived = ThreadSaveAddMessage;

            foreach (var peer in clientNode.Peers.Values)
            {
                var newPeer = new PeerModel
                {
                    Username = peer.Name
                };

                Peers.Add(newPeer);
            }

            SendCommand = new RelayCommand(async o =>
            {
                await Client.SendMessage(Message);

                var message = new Message
                {
                    PeerName = User.Name,
                    SentTime = DateTime.Now,
                    Content = Message
                };

                ThreadSaveAddMessage(message);
                Message = string.Empty;
            }, canExecute => true);

            DisconnectCommand = new RelayCommand(async o =>
            {
                Peers.Clear();
                Messages.Clear();
                await clientNode.Disconnect();
                clientNode.Stop();
                NavigateService.NavigateTo<MainViewModel>();
            }, canExecute => true);
        }

        private void ThreadSaveAddMessage(Message message)
        {
            var messageModel = new MessageModel()
            {
                Username = message.PeerName,
                Time = message.SentTime,
                Content = message.Content,
                IsNativeOrigin = User.Name == message.PeerName,
                FirstMessage = !Messages.Any() || Messages.Last().Username != message.PeerName
            };

            App.Current.Dispatcher.Invoke(() => { Messages.Add(messageModel); });
        }

        private void ThreadSaveAddPeer(Peer peer)
        {
            App.Current.Dispatcher.Invoke(() => Peers.Add(new PeerModel()
            {
                Username = peer.Name
            }));
        }

        private void ThreadSaveRemovePeer(Peer peer)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var removedPeer = Peers.First(x => x.Username == peer.Name);

                if (removedPeer != null)
                {
                    Peers.Remove(removedPeer);
                }
            });
        }
    }
}
