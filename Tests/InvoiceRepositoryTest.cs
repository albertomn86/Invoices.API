using Invoices.API;
using Invoices.API.Database;
using System;
using Tests.Tools;
using Xunit;

namespace Tests
{
    public class InvoiceRepositoryTest
    {
        [Fact]
        public void GivenAnInvalidIdShouldReturnNull()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoiceDto invoice = invoiceRepository.GetInvoices("5e3e0b21-e98a-4480-bfb7-49e8dc61f541");

            Assert.Null(invoice);
        }

        [Fact]
        public void GivenAValidIdShouldReturnAnInvoice()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoiceDto invoice = invoiceRepository.GetInvoices("5e3e0b21-e98a-4480-bfb7-49e8dc61f551");

            Assert.NotNull(invoice);
            Assert.Equal("5e3e0b21-e98a-4480-bfb7-49e8dc61f551", invoice.InvoiceId);
            Assert.Equal("Apple Inc.", invoice.Supplier);
            Assert.Equal("10/10/2019 13:30:01", invoice.DateIssued.ToString());
            Assert.Equal("EUR", invoice.Currency);
            Assert.Equal(1000m, invoice.Amount);
            Assert.Equal("New projector for conference room", invoice.Description);
        }

        [Fact]
        public void SaveAnInvoice()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoiceDto newInvoice = InvoiceCreator.Create("5e3e0b21-e98a-4480-bfb7-49e8dc61f552", "DELL", "2021-01-12T12:00:05", "USD", 500m, "New printer");

            _ = invoiceRepository.SaveInvoice(newInvoice);

            InvoiceDto savedInvoice = invoiceRepository.GetInvoices("5e3e0b21-e98a-4480-bfb7-49e8dc61f552");
            Assert.NotNull(savedInvoice);
            Assert.Equal("5e3e0b21-e98a-4480-bfb7-49e8dc61f552", savedInvoice.InvoiceId);
            Assert.Equal("DELL", savedInvoice.Supplier);
            Assert.Equal(Convert.ToDateTime("2021-01-12T12:00:05"), savedInvoice.DateIssued);
            Assert.Equal("USD", savedInvoice.Currency);
            Assert.Equal(500m, savedInvoice.Amount);
            Assert.Equal("New printer", savedInvoice.Description);
        }


        [Fact]
        public void DeleteAnExistingInvoice()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);

            invoiceRepository.DeleteInvoice("5e3e0b21-e98a-4480-bfb7-49e8dc61f551");

            InvoiceDto deletedInvoice = invoiceRepository.GetInvoices("5e3e0b21-e98a-4480-bfb7-49e8dc61f552");
            Assert.Null(deletedInvoice);
        }
    }
}
