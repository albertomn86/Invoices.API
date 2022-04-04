using Invoices.API.CurrencyExchange;

namespace Tests.CurrencyExchange.Tools
{
    public class FakeExchangeRateDownloader : IExchangeRateDownloader
    {
        private readonly decimal _returnValue;
        public FakeExchangeRateDownloader(decimal returnValue)
        {
            _returnValue = returnValue;
        }
        public decimal GetExchangeRate(string baseCurrency, string destCurrency)
        {
            return _returnValue;
        }
    }
}
