using Invoices.API;
using System;

namespace Tests.Tools
{
    public static class InvoiceCreator
    {
        public static InvoiceDto Create(string invoiceId, string supplier, string dateIssued, string currency, decimal amount, string description)
        {
            return new InvoiceDto()
            {
                InvoiceId = invoiceId,
                Supplier = supplier,
                DateIssued = Convert.ToDateTime(dateIssued),
                Description = description,
                Currency = currency,
                Amount = amount
            };
        }
    }
}
