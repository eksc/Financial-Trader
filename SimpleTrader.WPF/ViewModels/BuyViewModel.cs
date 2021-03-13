using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public BuyViewModel(IStockPriceService stockPriceService, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
        }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        private string _symbol;

        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
                OnPropertyChanged(nameof(CanSearchSymbol));
            }
        }

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

        private int _sharesToBuy;

        public int SharesToBuy
        {
            get { return _sharesToBuy; }
            set
            {
                _sharesToBuy = value;
                OnPropertyChanged(nameof(SharesToBuy));
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(CanBuyStock));
            }
        }

        public double TotalPrice
        {
            get
            {
                return SharesToBuy * StockPrice;
            }
        }

        public bool CanSearchSymbol => !string.IsNullOrEmpty(Symbol);
        public bool CanBuyStock => SharesToBuy > 0;
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

        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            StatusMessageViewModel.Dispose();

            base.Dispose();
        }

    }
}
