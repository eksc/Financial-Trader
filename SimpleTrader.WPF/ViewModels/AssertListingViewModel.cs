using SimpleTrader.WPF.State.Asserts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssertListingViewModel : ViewModelBase
    {
        private readonly AssertStore _assertStore;
        private readonly Func<IEnumerable<AssertViewModel>, IEnumerable<AssertViewModel>> _filterAssert;
        private readonly ObservableCollection<AssertViewModel> _asserts;

        public AssertListingViewModel(AssertStore assertStore) : this(assertStore, asserts => asserts) { }

        public AssertListingViewModel(AssertStore assertStore,
            Func<IEnumerable<AssertViewModel>, IEnumerable<AssertViewModel>> filterAssert)
        {
            _assertStore = assertStore;
            _filterAssert = filterAssert;
            _asserts = new ObservableCollection<AssertViewModel>();

            _assertStore.StateChanged += AssertStore_StateChanged;

            ResetAssets();
        }

        public IEnumerable<AssertViewModel> Asserts => _asserts;
        public double AccountBalance => _assertStore.AccountBalance;

        private void AssertStore_StateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssertViewModel> assertViewModels = _assertStore.AssertTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssertViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assertViewModels = _filterAssert(assertViewModels);

            _asserts.Clear();
            foreach (AssertViewModel viewModel in assertViewModels)
            {
                _asserts.Add(viewModel);
            }
        }

        

    }
}
