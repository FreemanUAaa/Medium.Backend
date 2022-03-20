using Medium.Users.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Core.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }

        DbSet<BioPhoto> BioPhotos { get; set; }

        DbSet<UserPhoto> UserPhotos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
