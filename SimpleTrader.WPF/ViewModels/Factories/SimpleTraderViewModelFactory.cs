using SimpleTrader.WPF.State.Navigations;
using SimpleTrader.WPF.ViewModels.Delegates;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class SimpleTraderViewModelFactory : ISimpleTraderViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _creatBuyViewModel;
        private readonly CreateViewModel<SellViewModel> _creatSellViewModel;

        public SimpleTraderViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<BuyViewModel> creatBuyViewModel, CreateViewModel<SellViewModel> creatSellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _creatBuyViewModel = creatBuyViewModel;
            _creatSellViewModel = creatSellViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Portfolio:
                    return _createPortfolioViewModel();
                case ViewType.Buy:
                    return _creatBuyViewModel();
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Sell:
                    return _creatSellViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel", "viewType");
            }
        }
    }
}
