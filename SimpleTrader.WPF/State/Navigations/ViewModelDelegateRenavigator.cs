using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Delegates;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Navigations
{
    public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly CreateViewModel<TViewModel> _createTraderViewModel;

        public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createTraderViewModel)
        {
            _navigator = navigator;
            _createTraderViewModel = createTraderViewModel;
        }

        public void Renavigator()
        {
            _navigator.CurrentViewModel = _createTraderViewModel();
        }
    }
}
