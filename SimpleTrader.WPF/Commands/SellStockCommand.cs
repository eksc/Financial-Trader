﻿using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;

            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.CanSellStock && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.StatusMessage = string.Empty;
            _viewModel.ErrorMessgae = string.Empty;

            try
            {
                string symbol = _viewModel.Symbol;
                int shares = _viewModel.SharesToSell;
                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);

                _accountStore.CurrentAccount = account;

                _viewModel.SearchResultSymbol = string.Empty;
                _viewModel.StatusMessage = $"Successfully purchased {shares} shares of {symbol}.";
            }
            catch (InsufficientSharesException ex)
            {
                _viewModel.ErrorMessgae = $"Account has insufficient shares. You have only {ex.AccountShares} shares.";
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessgae = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessgae = "Transaction failed.";
            }
        }

        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SellViewModel.CanSellStock))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
