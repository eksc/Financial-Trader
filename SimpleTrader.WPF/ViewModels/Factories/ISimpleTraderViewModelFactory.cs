using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public interface ISimpleTraderViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
