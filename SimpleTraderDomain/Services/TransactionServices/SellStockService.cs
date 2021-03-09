using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountDataService;


        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountDataService)
        {
            _stockPriceService = stockPriceService;
            _accountDataService = accountDataService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            int accountShares = GetAccountSharesForSymbol(seller, symbol);

            if (accountShares < shares)
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            seller.AssertTransactions.Add(new AssertTransaction()
            {
                Account = seller,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DareProcessed = DateTime.Now,
                IsPurchase = false,
                Shares = shares
            });

            seller.Balance += stockPrice * shares;

            await _accountDataService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            var accountTransactionsForSymbol = seller
                .AssertTransactions.Where(a => a.Asset.Symbol == symbol)
                .Sum(a => a.IsPurchase ? a.Shares : -a.Shares);

            return accountTransactionsForSymbol;
        }
    }
}
