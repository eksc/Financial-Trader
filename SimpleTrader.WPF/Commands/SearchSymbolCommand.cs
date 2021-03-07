using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class SearchSymbolCommand : AsyncCommandBase
    {
        private readonly BuyViewModel _buyViewModel;
        private IStockPriceService _stockPriceService;

        public SearchSymbolCommand(BuyViewModel buyViewModel, IStockPriceService stockPriceService)
        {
            _buyViewModel = buyViewModel;
            _stockPriceService = stockPriceService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                double stockPrice = await _stockPriceService.GetPrice(_buyViewModel.Symbol);
                _buyViewModel.SearchResultSymbol = _buyViewModel.Symbol.ToUpper();
                _buyViewModel.StockPrice = stockPrice;

            }
            catch (InvalidSymbolException)
            {
                _buyViewModel.ErrorMessgae = "Symbol does not exist.";
            }
            catch (Exception e)
            {

                _buyViewModel.ErrorMessgae = "Failes to get symbol information.";
            }
        }
    }
}
