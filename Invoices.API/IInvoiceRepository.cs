using System;
using System.Collections.Immutable;


namespace Invoices.API
{
    public interface IInvoiceRepository : IDisposable
    {
        ImmutableList<InvoiceDto> GetInvoices();
        InvoiceDto GetInvoices(string invoiceId);
        InvoiceDto SaveInvoice(InvoiceDto invoice);
        void DeleteInvoice(string invoiceId);
    }
}
