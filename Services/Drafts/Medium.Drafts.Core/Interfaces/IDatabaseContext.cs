using Medium.Drafts.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Drafts.Core.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Draft> Drafts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
