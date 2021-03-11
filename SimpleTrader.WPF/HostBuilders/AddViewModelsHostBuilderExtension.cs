using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigations;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Delegates;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((services) =>
             {
                 services.AddSingleton<HomeViewModel>(CreateHomeViewModel);

                 services.AddSingleton<BuyViewModel>();
                 services.AddSingleton<SellViewModel>();
                 services.AddSingleton<PortfolioViewModel>();
                 services.AddSingleton<AssertSummaryViewModel>();
                 services.AddScoped<MainViewModel>();

                 services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();

                 services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                 services.AddSingleton<CreateViewModel<BuyViewModel>>(services => () => services.GetRequiredService<BuyViewModel>());
                 services.AddSingleton<CreateViewModel<SellViewModel>>(services => () => services.GetRequiredService<SellViewModel>());
                 services.AddSingleton<CreateViewModel<PortfolioViewModel>>(services => () => services.GetRequiredService<PortfolioViewModel>());
                 services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
                 services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));

                 services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();
                 services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
                 services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();

             });

            return host;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(
                         MajorIndexListingViewModel.LoadMajorIndexViewModel(
                             services.GetRequiredService<IMajorIndexService>()),
                         services.GetRequiredService<AssertSummaryViewModel>());
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                         services.GetRequiredService<IAuthenticator>(),
                         services.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>(),
                         services.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(
                             services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
                             services.GetRequiredService<IAuthenticator>(),
                             services.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>()
                         );
        }
    }
}
