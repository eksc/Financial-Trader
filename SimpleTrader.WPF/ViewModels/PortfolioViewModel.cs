using SimpleTrader.WPF.State.Asserts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
        public PortfolioViewModel(AssertStore assertStore)
        {
            AssertListingViewModel = new AssertListingViewModel(assertStore);
        }

        public AssertListingViewModel AssertListingViewModel { get; }

        public override void Dispose()
        {
            AssertListingViewModel.Dispose();

            base.Dispose();
        }
    }
}
