namespace Invoices.API.CurrencyExchange
{
    public interface IExchangeRateDownloader
    {
        public decimal GetExchangeRate(string baseCurrency, string destCurrency);
    }
}