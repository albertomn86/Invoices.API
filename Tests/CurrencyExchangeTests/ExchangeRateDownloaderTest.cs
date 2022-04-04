using Invoices.API.CurrencyExchange;
using System.Collections.Generic;
using Xunit;

namespace Tests
{
    public class ExchangeRateDownloaderTest
    {
        [Fact]
        public void GivenAnInvalidBaseCurrencyMustAssert()
        {
            ExchangeRateDownloader exchangeDownloader = new ExchangeRateDownloader();

            Assert.Throws<KeyNotFoundException>(() => exchangeDownloader.GetExchangeRate("XXX", "USD"));
        }


        [Fact]
        public void GivenAnInvalidDestCurrencyMustAssert()
        {
            ExchangeRateDownloader exchangeDownloader = new ExchangeRateDownloader();

            Assert.Throws<KeyNotFoundException>(() => exchangeDownloader.GetExchangeRate("USD", "XXX"));
        }


        [Fact]
        public void GivenAValidDestCurrencyMustReturnAValue()
        {
            ExchangeRateDownloader exchangeDownloader = new ExchangeRateDownloader();

            decimal exchangeRate = exchangeDownloader.GetExchangeRate("USD", "EUR");

            Assert.NotEqual(0, exchangeRate);
        }
    }
}
