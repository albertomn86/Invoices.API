using Invoices.API.Database;
using System;
using System.Collections.Immutable;

namespace Invoices.API
{
    public class InvoiceRepository : IInvoiceRepository, IDisposable
    {
        private readonly AppDbContext _context;
        private bool disposedValue;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public InvoiceDto GetInvoices(string invoiceId)
        {
            return _context.Invoices.Find(invoiceId);
        }

        public ImmutableList<InvoiceDto> GetInvoices()
        {
            return _context.Invoices.ToImmutableList();
        }

        public InvoiceDto SaveInvoice(InvoiceDto invoice)
        {
            InvoiceDto invoiceToUpdate = _context.Invoices.Find(invoice.InvoiceId);
            if (invoiceToUpdate == null)
            {
                _context.Invoices.Add(invoice);
            }
            _context.SaveChanges();

            return invoice;
        }

        public void DeleteInvoice(string invoiceId)
        {
            InvoiceDto invoiceToDelete = _context.Invoices.Find(invoiceId);
            _context.Invoices.Remove(invoiceToDelete);
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
