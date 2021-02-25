using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigations;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IRoomSimpleTraderViewModelFactory _simpleTraderViewModelAbstractFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator,
            IRoomSimpleTraderViewModelFactory simpleTraderViewModelAbstractFactory)
        {
            _navigator = navigator;
            _simpleTraderViewModelAbstractFactory = simpleTraderViewModelAbstractFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _simpleTraderViewModelAbstractFactory.CreateViewModel(viewType);
            }
        }
    }
}
