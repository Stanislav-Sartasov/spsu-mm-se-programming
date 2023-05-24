using P2P_Chat_App.Helpers;
using P2P_Chat_App.Service;

namespace P2P_Chat_App.ViewModel
{
    internal class MainWindowViewModel : AViewModel
    {
        private INavigationService nagivateService;
        public INavigationService NavigateService
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

        public MainWindowViewModel(INavigationService nagivateService)
        {
            this.nagivateService = nagivateService;
            NavigateService.NavigateTo<NameViewModel>();
        }
    }
}
