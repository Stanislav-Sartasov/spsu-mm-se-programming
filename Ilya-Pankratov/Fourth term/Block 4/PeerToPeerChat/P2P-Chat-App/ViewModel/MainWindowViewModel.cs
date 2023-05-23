using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using P2P_Chat_App.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            NavigateService.NavigateTo<MainViewModel>();
        }
    }
}
