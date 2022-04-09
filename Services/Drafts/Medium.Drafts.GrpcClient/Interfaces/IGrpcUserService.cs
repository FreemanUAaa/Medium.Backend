using System;
using System.Threading.Tasks;

namespace Medium.Drafts.GrpcClient.Interfaces
{
    public interface IGrpcUserService
    {
        Task<bool> IsExistsAsync(Guid userId);
    }
}
