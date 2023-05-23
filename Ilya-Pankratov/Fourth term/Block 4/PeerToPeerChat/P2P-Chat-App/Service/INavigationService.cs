using P2P_Chat_App.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P_Chat_App.Service
{
    interface INavigationService
    {
        public AViewModel CurrentView { get;  }
        public void NavigateTo<T>() where T : AViewModel;
    }
}
