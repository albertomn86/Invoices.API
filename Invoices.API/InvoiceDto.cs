using System;
using System.ComponentModel.DataAnnotations;

namespace Invoices.API
{
    public sealed class InvoiceDto
    {
        [Key]
        public string InvoiceId { get; set; }
        public string Supplier { get; set; }
        public DateTime DateIssued { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
