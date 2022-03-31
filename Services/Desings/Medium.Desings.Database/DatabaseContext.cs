using Medium.Desings.Core.Interfaces;
using Medium.Desings.Core.Models;
using Medium.Desings.Database.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Medium.Desings.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Font> Fonts { get; set; }

        public DbSet<Header> Headers { get; set; }

        public DbSet<Desing> Desings { get; set; }

        public DbSet<HeaderColors> HeaderColors { get; set; }

        public DbSet<HeaderImage> HeaderImages { get; set; }

        public DbSet<HeaderName> HeaderNames { get; set; }

        public DbSet<HeaderNameLogo> HeaderNameLogos { get; set; }

        public DbSet<HeaderNameText> HeaderNameTexts { get; set; }

        public DbSet<MainColors> MainColors { get; set; }

        public DbSet<MainFonts> MainFonts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MainFontsConfiguration());
            modelBuilder.ApplyConfiguration(new MainColorsConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderColorsConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderNameConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderImageConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderNameLogoConfiguration());
            modelBuilder.ApplyConfiguration(new HeaderNameTextConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
