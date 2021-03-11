using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Asserts;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services => 
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAccountStore, AccountStore>();
                services.AddSingleton<AssertStore>();
            });

            return host;
        }
    }
}
