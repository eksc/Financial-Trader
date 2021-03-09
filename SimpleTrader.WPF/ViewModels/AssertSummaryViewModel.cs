using SimpleTrader.WPF.State.Asserts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssertSummaryViewModel : ViewModelBase
    {
        private readonly AssertStore _assertStore;

        public AssertSummaryViewModel(AssertStore assertStore)
        {
            _assertStore = assertStore;

            AssertListingViewModel = new AssertListingViewModel(assertStore, asserts => asserts.Take(3));

            _assertStore.StateChanged += AssertStore_StateChanged;
        }

        public double AccountBalance => _assertStore.AccountBalance;
        public AssertListingViewModel AssertListingViewModel { get; }

        private void AssertStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }
    }
}
