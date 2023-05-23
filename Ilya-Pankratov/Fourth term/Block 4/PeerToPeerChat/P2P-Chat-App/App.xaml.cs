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
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>(),
            });
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ChatViewModel>();
            services.AddSingleton<CurrentUserModel>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<Func<Type, AViewModel>>(serviceProvider => viewModelType => (AViewModel)serviceProvider.GetRequiredService(viewModelType));

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
