using Core;
using Core.Chat;
using Core.Data;
using Core.Network;
using Microsoft.Extensions.DependencyInjection;
using P2P_Chat_App.Helpers;
using P2P_Chat_App.Model;
using P2P_Chat_App.Service;
using P2P_Chat_App.View;
using P2P_Chat_App.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;
using NavigationService = P2P_Chat_App.Service.NavigationService;

namespace P2P_Chat_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Add window
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>(),
            });

            // Add view models
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ConnectViewModel>();
            services.AddSingleton<CreateViewModel>();
            services.AddSingleton<ChatViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<NameViewModel>();

            // Add user model
            services.AddSingleton<CurrentUserModel>(provider => new CurrentUserModel
            {
                Name = "Username",
                LocalPort = 0,
                LocalIpAddress = NetworkManager.GetLocalIp(),
                IsConnected = false
            }); ; ;


            // Add navigation function
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, AViewModel>>(serviceProvider => viewModelType => (AViewModel)serviceProvider.GetRequiredService(viewModelType));

            // Add ClientNode
            services.AddSingleton<IClient<Message, Peer>, ClientNode>(provider =>
            {
                var user = provider.GetRequiredService<CurrentUserModel>();
                return new ClientNode(user.Name); 
            });

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
