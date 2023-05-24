using P2P_Chat_App.Helpers;

namespace P2P_Chat_App.Service
{
    interface INavigationService
    {
        public AViewModel CurrentView { get; }
        public void NavigateTo<T>() where T : AViewModel;
    }
}
