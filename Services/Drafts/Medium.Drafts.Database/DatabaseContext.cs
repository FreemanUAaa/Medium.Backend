using Medium.Drafts.Core.Interfaces;
using Medium.Drafts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Medium.Drafts.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Draft> Drafts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
