namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel
    {
        string ErrorMessgae { set; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string Symbol { get; }
    }
}