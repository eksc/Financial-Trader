using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class BuyStockServiceTests
    {
        private BuyStockService _buyStockService;
        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _buyStockService = new BuyStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedSymbol = "bad_and_never_exist_symbol";
            Account buyer = CreateAccount(expectedSymbol, 10);
            _mockStockPriceService.Setup(s => s.GetPrice(expectedSymbol)).ThrowsAsync(new InvalidSymbolException(expectedSymbol));

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(
                () => _buyStockService.BuyStock(buyer, expectedSymbol, 10));
            string actualSymbol = exception.Symbol;

            Assert.AreEqual(expectedSymbol, actualSymbol);
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            Account buyer = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _buyStockService.BuyStock(buyer, It.IsAny<string>(), 5));
        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssertTransactions = new List<AssertTransaction>()
                {
                    new AssertTransaction()
                    {
                        Asset = new Asset()
                        {
                            Symbol = symbol
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }
    }
}
