using Microsoft.EntityFrameworkCore;

namespace Invoices.API.Database
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<InvoiceDto> Invoices { get; set; }
    }
}
