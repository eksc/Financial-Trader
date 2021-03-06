using SimpleTrader.Domain.Models;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Asserts
{
    public class AssertStore
    {
        private readonly IAccountStore _accountStore;

        public AssertStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }

        public double AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssertTransaction> AssertTransactions => _accountStore.CurrentAccount?.AssertTransactions ?? new List<AssertTransaction>();
        
        public event Action StateChanged;

    }
}
