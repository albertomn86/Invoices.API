using Invoices.API.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests.Tools
{
    internal class LocalContextGenerator
    {
        public AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            AppDbContext context = new AppDbContext(options);

            context.Add(InvoiceCreator.Create("5e3e0b21-e98a-4480-bfb7-49e8dc61f550", "DELL", "2019-10-12T12:00:05", "EUR", 1500m, "New TV for conference room"));
            context.Add(InvoiceCreator.Create("5e3e0b21-e98a-4480-bfb7-49e8dc61f551", "Apple Inc.", "2019-10-10T13:30:01", "EUR", 1000m, "New projector for conference room"));
            context.SaveChanges();

            return context;
        }
    }
}
