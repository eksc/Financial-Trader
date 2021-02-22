using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();

        public MainViewModel()
        {
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }
    }
}
