using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RoomSimpleTraderViewModelFactory : IRoomSimpleTraderViewModelFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private readonly BuyViewModel _buyViewModel;

        public RoomSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory,
            ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel, ISimpleTraderViewModelFactory<LoginViewModel> loginViewModelFactory)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
            _loginViewModelFactory = loginViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _homeViewModelFactory.CreateViewModel();
                case ViewType.Portfolio:
                    return _portfolioViewModelFactory.CreateViewModel();
                case ViewType.Buy:
                    return _buyViewModel;
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }
    }
}
