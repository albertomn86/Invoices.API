namespace Invoices.API.CurrencyExchange
{
    public class CurrencyConverter
    {
        private readonly IExchangeRateDownloader _exchangeRateDownloader;

        public CurrencyConverter()
        {
            _exchangeRateDownloader = new ExchangeRateDownloader();
        }

        public CurrencyConverter(IExchangeRateDownloader exchangeRateDownloader)
        {
            _exchangeRateDownloader = exchangeRateDownloader;
        }

        public decimal Convert(string fromCurrency, string toCurrency, decimal amount)
        {
            decimal rate = _exchangeRateDownloader.GetExchangeRate(fromCurrency, toCurrency);

            return decimal.Round(amount * rate, 2, System.MidpointRounding.AwayFromZero);
        }
    }
}
