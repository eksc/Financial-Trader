using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Navigations
{
    public class ViewModelFactoryRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly ISimpleTraderViewModelFactory<TViewModel> _simpleTraderViewModelFactory;

        public ViewModelFactoryRenavigator(INavigator navigator, ISimpleTraderViewModelFactory<TViewModel> simpleTraderViewModelFactory)
        {
            _navigator = navigator;
            _simpleTraderViewModelFactory = simpleTraderViewModelFactory;
        }

        public void Renavigator()
        {
            _navigator.CurrentViewModel = _simpleTraderViewModelFactory.CreateViewModel();
        }
    }
}
