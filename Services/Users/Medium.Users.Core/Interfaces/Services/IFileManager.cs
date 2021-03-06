using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Medium.Users.Core.Interfaces.Services
{
    public interface IFileManager
    {
        string UserSavePhotoPath { get; set; }

        string BioSavePhotoPath { get; set; }

        Task SaveFileAsync(IFormFile file, string path, CancellationToken cancellationToken = default);

        Task DeleteFileAsync(string path, CancellationToken cancellationToken = default);
    }
}
