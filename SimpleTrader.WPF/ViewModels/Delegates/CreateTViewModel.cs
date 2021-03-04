using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Delegates
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
}
