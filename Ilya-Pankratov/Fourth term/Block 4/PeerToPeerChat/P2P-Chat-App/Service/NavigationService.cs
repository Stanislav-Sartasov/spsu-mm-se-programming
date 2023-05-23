using P2P_Chat_App.Helpers;
using System;

namespace P2P_Chat_App.Service
{
    class NavigationService : ObservableObject, INavigationService
    {
        private AViewModel currentView;
        private readonly Func<Type, AViewModel> viewModelFactory;

        public AViewModel CurrentView {
            get
            {
                return currentView;
            }
            private set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, AViewModel> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : AViewModel
        {
            var viewModel = viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
