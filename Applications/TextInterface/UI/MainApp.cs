using System;
using Autofac;
using Common.Bll.Services.Interfaces;
using Data.Dto.Dtos;
using Data.EF.Contexts;
using Microsoft.Extensions.Configuration;
using Terminal.Gui;

namespace TextInterface.UI
{
    internal sealed class MainApp : IDisposable
    {
        internal static UserDto? User;
        private readonly IContainer _container;
        private readonly IConfiguration _configuration;

        public MainApp()
        {
            _configuration = GetConfiguration();
            ShopContext.ConnectionString = _configuration.GetConnectionString("ShopEntities");
            _container = AutofacConfig.GetContainer();
        }

        internal void StartApplication()
        {
            InitEvents();
            
            if (User is null) {
                Application.Run<LoginWindow>();
            }
        }

        public void Dispose()
        {
            Application.Shutdown ();
            _container.Dispose();
        }

        private void InitEvents()
        {
            EventManager.OnRequestLogin += async code => {
                var user = await _container.Resolve<IUserService>().LoginUserAsync(code);

                if (user is null) {
                    EventManager.RaiseLoginFailedEvent();
                    return;
                }

                EventManager.RaiseSuccessfulLoginEvent(user);
            };

            EventManager.OnSuccessfulLogin += user => {
                Application.Shutdown();
                MainApp.User = user;
                if(MainApp.User.IsAdmin)
                    Application.Run<AdminMainWindow>();
                else Application.Run<RegularUserMainWindow>();
            };

            EventManager.OnRequestLogout += () => {
                User = null;
                EventManager.RaiseSuccessfulLogoutEvent();
            };

            EventManager.OnSuccessfulLogout += () => {
                Application.Shutdown();
                Application.Run<LoginWindow>();
            };

            EventManager.OnShowProductList += () => {
                Application.Shutdown();
                Application.Run<ProductListWindow>();
            };
            
            EventManager.OnShowUserList += () => {
                Application.Shutdown();
                Application.Run<UserListWindow>();
            };

            EventManager.OnGoBack += requester =>
            {
                if (requester == typeof(ProductListWindow) || requester == typeof(UserListWindow))
                {
                    Application.Shutdown();
                    Application.Run<AdminMainWindow>();
                }

                if (requester == typeof(ShopCashWindow))
                {
                    if (User?.IsAdmin == true)
                    {
                        Application.Shutdown();
                        Application.Run<AdminMainWindow>();
                    }
                    else
                    {
                        Application.Shutdown();
                        Application.Run<RegularUserMainWindow>();
                    }
                }
            };

            EventManager.OnEnterCashRegisterMode += () =>
            {
                Application.Run<ShopCashWindow>();
            };
        }

        private IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}