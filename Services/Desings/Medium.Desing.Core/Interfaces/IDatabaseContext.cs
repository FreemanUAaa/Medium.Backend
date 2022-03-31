using Medium.Desings.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Desings.Core.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Font> Fonts { get; set; }

        DbSet<Header> Headers { get; set; }

        DbSet<Desing> Desings { get; set; }

        DbSet<HeaderColors> HeaderColors { get; set; }

        DbSet<HeaderImage> HeaderImages { get; set; }

        DbSet<HeaderName> HeaderNames { get; set; }

        DbSet<HeaderNameLogo> HeaderNameLogos { get; set; }

        DbSet<HeaderNameText> HeaderNameTexts { get; set; }

        DbSet<MainColors> MainColors { get; set; }

        DbSet<MainFonts> MainFonts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
