using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;

namespace P2P_Chat_App.ViewModel
{
    internal class MainViewModel : AViewModel
    {
        private INavigationService nagivateService;
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

        public RelayCommand NavigateToConnectCommand { get; set; }
        public RelayCommand NavigateToCreateCommand { get; set; }

        public MainViewModel(INavigationService nagivateService, CurrentUserModel user)
        {
            NavigateToConnectCommand = new RelayCommand(o =>
            {
                NagivateService.NavigateTo<ConnectViewModel>();
            }, canExecute => true);

            NavigateToCreateCommand = new RelayCommand(o =>
            {
                NagivateService.NavigateTo<CreateViewModel>();
            }, canExecute => true);

            this.nagivateService = nagivateService;
        }
    }
}
