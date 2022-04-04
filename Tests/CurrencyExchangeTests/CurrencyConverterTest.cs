using Invoices.API.CurrencyExchange;
using Tests.CurrencyExchange.Tools;
using Xunit;

namespace Tests
{
    public class CurrencyConverterTest
    {
        [Fact]
        public void GivenAnAmount_ConvertEURToUSD()
        {
            decimal amountEUR = 100m;
            decimal fakeRate = 1.17m;
            decimal expectedUSD = amountEUR * fakeRate;

            IExchangeRateDownloader rateDownloader = new FakeExchangeRateDownloader(fakeRate);
            CurrencyConverter converter = new CurrencyConverter(rateDownloader);

            decimal amountUSD = converter.Convert("EUR", "USD", amountEUR);

            Assert.Equal(expectedUSD, amountUSD);
        }
    }
}
