using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace Medium.Drafts.Tests.Database
{
    public static class DatabaseContextFactory
    {
        public static IDatabaseContext Create()
        {
            DbContextOptions<DatabaseContext> options =
                new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            DatabaseContext context = new DatabaseContext(options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
