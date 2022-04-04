using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace Invoices.API.Controllers
{
    [Route("")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet("{invoiceId}")]
        public ActionResult<InvoiceDto> Get(string invoiceId)
        {
            InvoiceDto invoice = _invoiceRepository.GetInvoices(invoiceId);
            if (invoice != null)
            {
                return Ok(invoice);
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<ImmutableList<InvoiceDto>> Get()
        {
            return Ok(_invoiceRepository.GetInvoices());
        }

        [HttpPost]
        public ActionResult Post([FromBody] InvoiceDto invoice)
        {
            if (ModelState.IsValid)
            {
                InvoiceDto savedInvoice = _invoiceRepository.SaveInvoice(invoice);
                return CreatedAtAction("Get", new { savedInvoice.InvoiceId }, savedInvoice);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{invoiceId}")]
        public ActionResult Remove(string invoiceId)
        {
            InvoiceDto invoice = _invoiceRepository.GetInvoices(invoiceId);
            if (invoice != null)
            {
                _invoiceRepository.DeleteInvoice(invoiceId);
                return Ok();
            }
            return NotFound();
        }
    }
}