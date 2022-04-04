using Invoices.API;
using Invoices.API.Controllers;
using Invoices.API.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Tests.Tools;
using Xunit;

namespace Tests
{
    public class ControllerTest
    {
        [Fact]
        public void GetMustReturnOk()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Get();

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetMustReturnAllInvoices()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Get().Result as OkObjectResult;

            ImmutableList<InvoiceDto> invoices = Assert.IsType<ImmutableList<InvoiceDto>>(response.Value);
            Assert.Equal(2, invoices.Count);
        }

        [Fact]
        public void GetValidInvoiceMustReturnOk()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Get("5e3e0b21-e98a-4480-bfb7-49e8dc61f551");

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetInvalidInvoiceMustReturnNotFound()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository); ;

            var response = controller.Get("5e3e0b21-e98a-4480-bfb7-49e8dc61f510");

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void GetValidInvoiceMustReturnRightInvoice()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Get("5e3e0b21-e98a-4480-bfb7-49e8dc61f550").Result as OkObjectResult;

            InvoiceDto invoice = Assert.IsType<InvoiceDto>(response.Value);
            Assert.Equal("New TV for conference room", invoice.Description);
        }

        [Fact]
        public void AddValidInvoiceMustReturnCreatedResponse()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);
            InvoiceDto newInvoice = InvoiceCreator.Create("5e3e0b21-e98a-4480-bfb7-49e8dc61f552", "DELL", "2021-01-12T12:00:05", "USD", 500m, "New printer");

            var response = controller.Post(newInvoice);

            Assert.IsType<CreatedAtActionResult>(response);
        }

        [Fact]
        public void AddValidInvoiceMustReturnCreatedInvoice()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);
            InvoiceDto newInvoice = InvoiceCreator.Create("5e3e0b21-e98a-4480-bfb7-49e8dc61f552", "DELL", "2021-01-12T12:00:05", "USD", 500m, "New printer");

            var response = controller.Post(newInvoice) as CreatedAtActionResult;

            Assert.Equal("5e3e0b21-e98a-4480-bfb7-49e8dc61f552", (response.Value as InvoiceDto).InvoiceId);
        }

        [Fact]
        public void AddInvalidInvoiceMustReturnBadRequest()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);
            InvoiceDto newInvoice = new InvoiceDto()
            {
                Supplier = "DELL"
            };
            controller.ModelState.AddModelError("Description", "Required");

            var response = controller.Post(newInvoice);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void DeleteValidInvoiceMustReturnOkResponse()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Remove("5e3e0b21-e98a-4480-bfb7-49e8dc61f551");

            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void DeleteNotExistingInvoiceMustReturnNotFound()
        {
            using AppDbContext context = new LocalContextGenerator().GetContext();
            InvoiceRepository invoiceRepository = new InvoiceRepository(context);
            InvoicesController controller = new InvoicesController(invoiceRepository);

            var response = controller.Remove("5e3e0b21-e98a-4480-bfb7-49e8dc61f510");

            Assert.IsType<NotFoundResult>(response);
        }
    }
}