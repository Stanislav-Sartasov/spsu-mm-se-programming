﻿using Core.Chat;
using Core.Data;
using Core;
using Core.Network;
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
    internal class NameViewModel : AViewModel
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
        public RelayCommand NextCommand { get; set; }

        public NameViewModel(INavigationService nagivateService, CurrentUserModel user)
        {

            NextCommand = new RelayCommand(o =>
            {
                NagivateService.NavigateTo<MainViewModel>();
                user.Name = Username;
            }, canExecute => true);

            this.nagivateService = nagivateService;
            this.user = user;
        }
    }
}
