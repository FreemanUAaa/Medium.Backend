using Medium.Users.Core.Interfaces;
using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Medium.Users.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BioPhoto> BioPhotos { get; set; }
        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
