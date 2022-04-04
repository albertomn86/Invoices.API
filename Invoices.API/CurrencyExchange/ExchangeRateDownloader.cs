using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Invoices.API.CurrencyExchange
{
    public class ExchangeRateDownloader : IExchangeRateDownloader
    {
        public decimal GetExchangeRate(string baseCurrency, string destCurrency)
        {
            Dictionary<string, double> ratesTable = GetCurrencyRatesFromAPI(baseCurrency);

            if (!ratesTable.TryGetValue(destCurrency, out double rate))
            {
                throw new KeyNotFoundException($"Currency '{destCurrency}' not found.");
            }

            return Convert.ToDecimal(rate);
        }


        private Dictionary<string, double> GetCurrencyRatesFromAPI(string baseCurrency)
        {
            Dictionary<string, double> ratesTable = new Dictionary<string, double>();

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString($"https://api.exchangerate.host/latest?base={baseCurrency}");

                using JsonDocument document = JsonDocument.Parse(json);
                JsonElement root = document.RootElement;

                if (root.GetProperty("base").ToString() != baseCurrency)
                {
                    throw new KeyNotFoundException($"Base currency '{baseCurrency}' not found.");
                }

                JsonElement ratesList = root.GetProperty("rates");
                ratesTable = JsonSerializer.Deserialize<Dictionary<string, double>>(ratesList.ToString());
            }

            return ratesTable;
        }
    }
}
