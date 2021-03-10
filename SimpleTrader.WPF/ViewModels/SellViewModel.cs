using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Asserts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public AssertListingViewModel AssertListingViewModel { get; set; }


        public SellViewModel(AssertStore assertStore, IStockPriceService stockPriceService,
            IAccountStore accountStore, ISellStockService sellStockService)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            AssertListingViewModel = new AssertListingViewModel(assertStore);
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);
        }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand SellStockCommand { get; set; }

        private AssertViewModel _selectedAssert;
        public AssertViewModel SelectedAssert
        {
            get => _selectedAssert;
            set
            {
                _selectedAssert = value;
                OnPropertyChanged(nameof(SelectedAssert));
            }
        }

        public string Symbol => SelectedAssert?.Symbol;

        private string _searchResultSymbol = string.Empty;

        public string SearchResultSymbol
        {
            get { return _searchResultSymbol; }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }

        private double _stockPrice;

        public double StockPrice
        {
            get { return _stockPrice; }
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _sharesToSell;

        public int SharesToSell
        {
            get { return _sharesToSell; }
            set
            {
                _sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice
        {
            get
            {
                return SharesToSell * StockPrice;
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessgae
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public MessageViewModel StatusMessageViewModel { get; set; }
        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }
    }
}
