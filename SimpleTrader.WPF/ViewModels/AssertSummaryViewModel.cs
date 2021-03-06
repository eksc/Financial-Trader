﻿using SimpleTrader.WPF.State.Asserts;
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
        private readonly ObservableCollection<AssertViewModel> _topAsserts;

        public AssertSummaryViewModel(AssertStore assertStore)
        {
            _assertStore = assertStore;

            _topAsserts = new ObservableCollection<AssertViewModel>();

            _assertStore.StateChanged += AssertStore_StateChanged;

            ResetAssets();
        }

        public double AccountBalance => _assertStore.AccountBalance;
        public IEnumerable<AssertViewModel> TopAsserts => _topAsserts;

        private void AssertStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssertViewModel> assertViewModels = _assertStore.AssertTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssertViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.Shares : -a.Shares)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares)
                .Take(3);

            _topAsserts.Clear();
            foreach(AssertViewModel viewModel in assertViewModels)
            {
                _topAsserts.Add(viewModel);
            }
        }
    }
}
